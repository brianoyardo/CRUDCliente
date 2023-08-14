using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApi.Models;

namespace WebApi.Data
{
    public class ClienteData
    {
        public static async Task<DataTable> ListarCliente()
        {
            DataTable table = new DataTable();
            SqlConnection con = GlobalesData.CreateDatabase();
            await con.OpenAsync();
            try
            {
                string consulta = "dbo.LISTAR_CLIENTES"; // Aqui va el nombre del procedimiento almacenado
                using (SqlCommand cad = new SqlCommand(consulta, con))
                {
                    cad.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cad);

                    da.Fill(table);
                    da.Dispose();
                    await con.CloseAsync();
                }
                return table;
            }
            catch (Exception ex)
            {
                table = new DataTable();
                await con.CloseAsync();
                return table;
            }
        }
        public static async Task<Respuesta> RegistrarCliente(Cliente ent)
        {
            Respuesta res = new Respuesta();

            SqlConnection con = GlobalesData.CreateDatabase();
            await con.OpenAsync();
            try
            {
                string consulta = "dbo.INSERTAR_CLIENTES"; // Aqui va el nombre del procedimiento almacenado
                using (SqlCommand cad = new SqlCommand(consulta, con))
                {
                    cad.CommandType = CommandType.StoredProcedure;
                    cad.Parameters.AddWithValue("@ClienteId", ent.ClienteId);
                    cad.Parameters.AddWithValue("@Nombre", ent.Nombre);
                    cad.Parameters.AddWithValue("@Direccion", ent.Direccion);

                    await cad.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    res.Mensaje = "Registro Correcto";
                    res.Error = false;
                    res.Obj = new object();
                }
                return res;
            }
            catch (Exception ex)
            {
                res.Mensaje =ex.Message.ToString();
                res.Error = true;
                res.Obj = new object();
                return res;
            }
        }

        public static async Task<Respuesta> EliminarCliente(Cliente ent)
        {
            Respuesta res = new Respuesta();

            SqlConnection con = GlobalesData.CreateDatabase();
            await con.OpenAsync();
            try
            {
                string consulta = "dbo.ELIMINAR_CLIENTE"; // Aqui va el nombre del procedimiento almacenado
                using (SqlCommand cad = new SqlCommand(consulta, con))
                {
                    cad.CommandType = CommandType.StoredProcedure;
                    cad.Parameters.AddWithValue("@Id", ent.ClienteId);
                    

                    await cad.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    res.Mensaje = "La eliminacion se Realizo con Exito. Todo Correcto.";
                    res.Error = false;
                    res.Obj = new object();
                }
                return res;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message.ToString();
                res.Error = true;
                res.Obj = new object();
                return res;
            }
        }

        public static async Task<Respuesta> ActualizarCliente(Cliente ent)
        {
            Respuesta res = new Respuesta();

            SqlConnection con = GlobalesData.CreateDatabase();
            await con.OpenAsync();
            try
            {
                string consulta = "dbo.ACTUALIZAR_CLIENTE"; // Aqui va el nombre del procedimiento almacenado
                using (SqlCommand cad = new SqlCommand(consulta, con))
                {
                    cad.CommandType = CommandType.StoredProcedure;
                    cad.Parameters.AddWithValue("@Id", ent.ClienteId);
                    cad.Parameters.AddWithValue("@Nombre", ent.Nombre);
                    cad.Parameters.AddWithValue("@Direccion", ent.Direccion);


                    await cad.ExecuteNonQueryAsync();
                    await con.CloseAsync();
                    res.Mensaje = "La Actualizacion se Realizo con Exito. Datos Actualizados.";
                    res.Error = false;
                    res.Obj = new object();
                }
                return res;
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message.ToString();
                res.Error = true;
                res.Obj = new object();
                return res;
            }
        }
    }
}
