using System.Data;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.TiendaNegocio
{
    public interface IServicio
    {
        Task<DataTable> ListarCliente();
        Task<Respuesta> RegistrarCliente(Cliente ent);
        Task<Respuesta> EliminarCliente(Cliente ent);
        Task<Respuesta> ActualizarCliente(Cliente ent);
    }
}
