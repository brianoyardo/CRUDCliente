using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.TiendaNegocio
{
    public partial class Servicio : IServicio
    {
        public async Task<Respuesta> ActualizarCliente(Cliente ent)
        {
            return await ClienteData.ActualizarCliente(ent);
        }

        public async Task<Respuesta> EliminarCliente(Cliente ent)
        {
            return await ClienteData.EliminarCliente(ent);
        }

        public async Task<DataTable> ListarCliente() 
        {
            return await ClienteData.ListarCliente();
        }

        public async Task<Respuesta> RegistrarCliente(Cliente ent)
        {
            return await ClienteData.RegistrarCliente(ent);
        }
    }
}
