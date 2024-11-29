using Microsoft.EntityFrameworkCore;
using Proyect.Models;

namespace Proyect.Servicios.Contrato
{
    public interface IClienteService
    {
        Task<Cliente> GetCliente(string Correo, string Contrasena);
        Task<Cliente> GetClienteByEmail(string correo);
        Task<Cliente> SaveCliente(Cliente modelo);
        Task<bool> UpdateCliente(Cliente cliente);
    }
}
