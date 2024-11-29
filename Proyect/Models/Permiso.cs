using System;
using System.Collections.Generic;

namespace Proyect.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string? NomPermiso { get; set; }

    public string Descripcion { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<RolesPermiso> PermisosRoles { get; set; } = new List<RolesPermiso>();
}
