using System;
using System.Collections.Generic;

namespace Proyect.Models;

public partial class Reserva
{
    public string? NroDocumentoCliente { get; set; }

    public int? NroDocumentoUsuario { get; set; }

    public int IdReserva { get; set; }

    public DateTime FechaReserva { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public decimal Descuento { get; set; }

    public int NoPersonas { get; set; }

    public int IdPaquete { get; set; }

    public int IdEstadoReserva { get; set; }

    public int IdMetodoPago { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    public virtual ICollection<DetalleHabitacione> DetalleHabitaciones { get; set; } = new List<DetalleHabitacione>();

    public virtual ICollection<DetallePaquete> DetallePaquetes { get; set; } = new List<DetallePaquete>();

    public virtual ICollection<DetalleServicio> DetalleServicios { get; set; } = new List<DetalleServicio>();

    public virtual EstadoReserva IdEstadoReservaNavigation { get; set; } = null!;

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual Paquete IdPaqueteNavigation { get; set; } = null!;

    public virtual Cliente? NroDocumentoClienteNavigation { get; set; }

    public virtual Usuario? NroDocumentoUsuarioNavigation { get; set; }
}
