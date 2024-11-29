using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyect.Models;

public partial class Abono
{
    public int IdAbono { get; set; }

    public int IdReserva { get; set; }

    public DateTime FechaAbono { get; set; }

    public decimal Valordeuda { get; set; }

    public decimal Porcentaje { get; set; }

    public decimal Pendiente { get; set; }

    public decimal ValorAbono { get; set; }

    public byte[] Comprobante { get; set; }

    public bool Estado { get; set; }

    public virtual Reserva IdReservaNavigation { get; set; } = null!;

    public virtual Abono IdEstadoAbonoNavigation { get; set; }
}


