﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyect.Models;

namespace Proyect.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly ProyectContext _context;

        public HabitacionesController(ProyectContext context)
        {
            _context = context;
        }

        // GET: Habitaciones
        public async Task<IActionResult> Index()
        {
            var proyectContext = _context.Habitaciones.Include(h => h.IdTipoHabitacionNavigation);
            return View(await proyectContext.ToListAsync());
        }

        // GET: Habitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacione = await _context.Habitaciones
                .Include(h => h.IdTipoHabitacionNavigation)
                .Include(h => h.HabitacionMuebles)
                .ThenInclude(hm => hm.IdMuebleNavigation)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);

            if (habitacione == null)
            {
                return NotFound();
            }

            return View(habitacione);
        }

        // GET: Habitaciones/Create
        public IActionResult Create()
        {
            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitaciones, "IdTipoHabitacion", "Nombre");
            ViewData["Muebles"] = _context.Muebles.Where(s => s.Estado == false).Select(m => new
            {
                Value = m.IdMueble.ToString(),
                Text = $"{m.Nombre} (Cantidad disponible: {m.Cantidad})",
                m.IdMueble,
                m.Nombre,
                m.Cantidad
            }).ToList();
            return View();
        }

        // POST: Habitaciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabitacion,Nombre,IdTipoHabitacion,Estado,Descripcion,Precio")] Habitacione habitacione, int[] mueblesSeleccionados, int[] cantidadMuebles)
        {
            if (ModelState.IsValid)
            {
                // Guardar la habitación
                _context.Add(habitacione);
                await _context.SaveChangesAsync();

                // Guardar la relación con los muebles y actualizar las cantidades
                if (mueblesSeleccionados != null)
                {
                    for (int i = 0; i < mueblesSeleccionados.Length; i++)
                    {
                        var muebleId = mueblesSeleccionados[i];
                        var cantidad = cantidadMuebles != null && i < cantidadMuebles.Length ? cantidadMuebles[i] : 0; // Default cantidad

                        var habitacionMueble = new HabitacionMueble
                        {
                            IdHabitacion = habitacione.IdHabitacion,
                            IdMueble = muebleId,
                            Cantidad = cantidad // Cantidad asignada a la habitación
                        };

                        _context.HabitacionMuebles.Add(habitacionMueble);

                        // Actualizar la cantidad de muebles en la tabla Muebles
                        var mueble = await _context.Muebles.FindAsync(muebleId);
                        if (mueble != null)
                        {
                            mueble.Cantidad -= cantidad; // Restar la cantidad seleccionada
                            _context.Update(mueble); // Marcar el mueble como modificado
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitaciones, "IdTipoHabitacion", "Nombre", habitacione.IdTipoHabitacion);
            ViewData["Muebles"] = new SelectList(_context.Muebles.Where(s => s.Estado == false), "IdMueble", "Nombre");
            return View(habitacione);
        }

        // GET: Habitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacione = await _context.Habitaciones
                .Include(h => h.HabitacionMuebles)
                .ThenInclude(hm => hm.IdMuebleNavigation) // Incluir información de muebles
                .FirstOrDefaultAsync(h => h.IdHabitacion == id);

            if (habitacione == null)
            {
                return NotFound();
            }

            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitaciones, "IdTipoHabitacion", "Nombre", habitacione.IdTipoHabitacion);

            // Obtener todos los muebles y la cantidad asignada en la habitación
            var muebles = await _context.Muebles.ToListAsync();
            var mueblesSeleccionados = habitacione.HabitacionMuebles.Select(hm => new
            {
                hm.IdMueble,
                hm.Cantidad
            }).ToDictionary(m => m.IdMueble, m => m.Cantidad);

            ViewData["Muebles"] = muebles.Select(m => new {
                Id = m.IdMueble,
                Nombre = m.Nombre,
                CantidadDisponible = m.Cantidad,
                CantidadEnHabitacion = mueblesSeleccionados.ContainsKey(m.IdMueble) ? mueblesSeleccionados[m.IdMueble] : 0,
                Seleccionado = mueblesSeleccionados.ContainsKey(m.IdMueble)
            }).ToList();

            return View(habitacione);
        }


        // POST: Habitaciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHabitacion,Nombre,IdTipoHabitacion,Estado,Descripcion,Precio")] Habitacione habitacione, int[] mueblesSeleccionados, int[] cantidadMuebles)
        {
            if (id != habitacione.IdHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar la habitación
                    _context.Update(habitacione);
                    await _context.SaveChangesAsync();

                    // Obtener los muebles anteriores
                    var mueblesAnteriores = _context.HabitacionMuebles.Where(hm => hm.IdHabitacion == id).ToList();

                    // Actualizar muebles existentes o agregar nuevos
                    for (int i = 0; i < mueblesSeleccionados.Length; i++)
                    {
                        var muebleId = mueblesSeleccionados[i];
                        var nuevaCantidad = cantidadMuebles[i];

                        // Buscar mueble en la relación HabitacionMuebles
                        var muebleHabitacion = mueblesAnteriores.FirstOrDefault(hm => hm.IdMueble == muebleId);
                        var mueble = await _context.Muebles.FindAsync(muebleId);

                        if (muebleHabitacion != null)
                        {
                            // Calcular la diferencia de cantidad y actualizar la cantidad en Muebles
                            var diferencia = nuevaCantidad - muebleHabitacion.Cantidad;
                            muebleHabitacion.Cantidad = nuevaCantidad;
                            mueble.Cantidad -= diferencia;
                        }
                        else
                        {
                            // Si el mueble no existe en la relación, lo agrega
                            var nuevoMueble = new HabitacionMueble
                            {
                                IdHabitacion = habitacione.IdHabitacion,
                                IdMueble = muebleId,
                                Cantidad = nuevaCantidad
                            };
                            _context.HabitacionMuebles.Add(nuevoMueble);
                            mueble.Cantidad -= nuevaCantidad;
                        }

                        _context.Update(mueble);
                    }

                    // Eliminar muebles que ya no están seleccionados y ajustar la cantidad en Muebles
                    var mueblesAEliminar = mueblesAnteriores.Where(hm => !mueblesSeleccionados.Contains(hm.IdMueble)).ToList();
                    foreach (var muebleAEliminar in mueblesAEliminar)
                    {
                        var mueble = await _context.Muebles.FindAsync(muebleAEliminar.IdMueble);
                        if (mueble != null)
                        {
                            mueble.Cantidad += muebleAEliminar.Cantidad;
                            _context.HabitacionMuebles.Remove(muebleAEliminar);
                            _context.Update(mueble);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacioneExists(habitacione.IdHabitacion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdTipoHabitacion"] = new SelectList(_context.TipoHabitaciones, "IdTipoHabitacion", "Nombre", habitacione.IdTipoHabitacion);
            ViewData["Muebles"] = _context.Muebles
                .Where(m => m.Estado == false)
                .Select(m => new {
                    IdMueble = m.IdMueble,
                    Nombre = m.Nombre,
                    Cantidad = m.Cantidad,
                    Seleccionado = mueblesSeleccionados.Contains(m.IdMueble)
                }).ToList();

            return View(habitacione);
        }

        public async Task<IActionResult> AsignarMuebleAHabitacion(int Idhabitacion, int Idmueble, int cantidad)
        {
            // Verificar si la habitación y el mueble existen
            var habitacion = await _context.Habitaciones.FindAsync(Idhabitacion);
            var mueble = await _context.Muebles.FindAsync(Idmueble);

            if (habitacion == null || mueble == null)
            {
                return NotFound(); // O maneja el error como prefieras
            }

            // Verificar si hay suficiente cantidad de muebles
            if (mueble.Cantidad < cantidad)
            {
                return BadRequest("No hay suficiente cantidad de muebles disponibles.");
            }

            // Asignar el mueble a la habitación (esto dependerá de tu lógica de asignación)
            var asignacionMueble = new HabitacionMueble
            {
                IdHabitacion = Idhabitacion,
                IdMueble = Idmueble,
                Cantidad = cantidad
            };

            // Añadir la asignación a la base de datos
            _context.HabitacionMuebles.Add(asignacionMueble);
            await _context.SaveChangesAsync();

            // Actualizar la cantidad de muebles disponibles
            mueble.Cantidad -= cantidad;
            await _context.SaveChangesAsync();

            return Ok("Mueble asignado correctamente.");
        }

        public async Task<IActionResult> EliminarMuebleDeHabitacion(int IdasignacionId)
        {
            // Buscar la asignación
            var asignacion = await _context.HabitacionMuebles.FindAsync(IdasignacionId);

            if (asignacion == null)
            {
                return NotFound(); // O maneja el error como prefieras
            }

            // Recuperar el mueble asignado
            var mueble = await _context.Muebles.FindAsync(asignacion.IdMueble);

            // Eliminar la asignación
            _context.HabitacionMuebles.Remove(asignacion);
            await _context.SaveChangesAsync();

            // Actualizar la cantidad de muebles disponibles
            mueble.Cantidad += asignacion.Cantidad;
            await _context.SaveChangesAsync();

            return Ok("Mueble eliminado de la habitación correctamente.");
        }


        // GET: Habitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacione = await _context.Habitaciones
                .Include(h => h.IdTipoHabitacionNavigation)
                .Include(h => h.HabitacionMuebles)
                .ThenInclude(hm => hm.IdMuebleNavigation)
                .FirstOrDefaultAsync(m => m.IdHabitacion == id);

            if (habitacione == null)
            {
                return NotFound();
            }

            return View(habitacione);
        }

        // POST: Habitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Obtener la habitación a eliminar junto con sus muebles asociados
            var habitacione = await _context.Habitaciones
                .Include(h => h.HabitacionMuebles)
                .FirstOrDefaultAsync(h => h.IdHabitacion == id);

            if (habitacione == null)
            {
                return NotFound();
            }

            var paqueteRelacionado = await _context.PaquetesHabitaciones.AnyAsync(ph => ph.IdHabitacion == id);
            if (paqueteRelacionado)
            {
                TempData["ErrorMessage"] = "No se puede eliminar la habitación porque está asociada a uno o más paquetes.";
                return RedirectToAction(nameof(Index));
            }

            // Devolver la cantidad de cada mueble a la tabla Muebles
            foreach (var muebleHabitacion in habitacione.HabitacionMuebles)
            {
                var mueble = await _context.Muebles.FindAsync(muebleHabitacion.IdMueble);
                if (mueble != null)
                {
                    mueble.Cantidad += muebleHabitacion.Cantidad; // Devolver cantidad
                    _context.Update(mueble);
                }
            }

            // Eliminar los registros de la tabla intermedia HabitacionMuebles
            _context.HabitacionMuebles.RemoveRange(habitacione.HabitacionMuebles);

            // Eliminar la habitación
            _context.Habitaciones.Remove(habitacione);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "La habitación se eliminó correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ActualizarEstado(int id, bool estado)
        {
            var habitacion = _context.Habitaciones.Find(id);
            if (habitacion != null)
            {
                habitacion.Estado = estado;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Habitación no encontrada." });
        }


        private bool HabitacioneExists(int id)
        {
            return _context.Habitaciones.Any(e => e.IdHabitacion == id);
        }
    }
}
