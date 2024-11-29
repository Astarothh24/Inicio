using Microsoft.EntityFrameworkCore;
using Proyect.Models;
using Proyect.Servicios.Contrato;
using System.Threading.Tasks;

namespace Proyect.Servicios.Implementacion
{
    public class ClienteService : IClienteService
    {
        private readonly ProyectContext _bdContext;

        public ClienteService(ProyectContext bdContext)
        {
            _bdContext = bdContext;
        }

        public async Task<Cliente> GetCliente(string Correo, string Contrasena)
        {
            Cliente cliente_encontrado = await _bdContext.Clientes
                .Where(u => u.Correo == Correo && u.Contrasena == Contrasena)
                .FirstOrDefaultAsync();

            return cliente_encontrado;
        }

        public async Task<Cliente> SaveCliente(Cliente modelo)
        {
            _bdContext.Clientes.Add(modelo);
            await _bdContext.SaveChangesAsync();
            return modelo;
        }

        // Cambiamos el tipo de retorno a Task
        public async Task<bool> UpdateCliente(Cliente cliente)
        {
            // Buscar al cliente en la base de datos por correo
            var clienteExistente = await _bdContext.Clientes.FirstOrDefaultAsync(c => c.Correo == cliente.Correo);

            if (clienteExistente == null)
            {
                // Si no se encuentra el cliente, retornar false
                return false;
            }

            // Actualizar la contraseña del cliente
            clienteExistente.Contrasena = cliente.Contrasena;

            // Guardar los cambios en la base de datos
            try
            {
                _bdContext.Clientes.Update(clienteExistente);
                await _bdContext.SaveChangesAsync(); // Guardar cambios en la base de datos
                return true;  // Si todo va bien, devolver true
            }
            catch (Exception)
            {
                // En caso de error, devolver false
                return false;
            }
        }

        public async Task<Cliente> GetClienteByEmail(string correo)
        {
            Cliente cliente_encontrado = await _bdContext.Clientes
                .Where(u => u.Correo == correo)
                .FirstOrDefaultAsync();

            return cliente_encontrado;
        }
    }
}