using System.Data;
using System.Data.SqlClient;
using tiendaapi.Conexion;
using tiendaapi.Modelo;

namespace tiendaapi.Datos
{
    public class Dproductos
    {
        Conexionbd cn = new Conexionbd();
        public async Task<List<Mproductos>> Mostrarproductos() 
        { 
            var lista = new List<Mproductos>();
            using (var sql = new SqlConnection(cn.cadenaSql())) {
                using (var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mproducts = new Mproductos();
                            mproducts.id = (int)item["id"];
                            mproducts.descripcion = (string)item["descripcion"];
                            mproducts.precio = (decimal)item["precio"];
                            lista.Add(mproducts); 
                        }
                    }
                }
            }
            return lista;
        }
        public async Task InsertarProductos(Mproductos parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSql()))
            {
                using (var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue
                        ("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
        public async Task EditarProductos(Mproductos parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSql()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                { 
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parametros.id);
                    cmd.Parameters.AddWithValue("precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    
                }
            }
        }
        public async Task EliminarProductos(Mproductos parametros)
        {

            using (var sql = new SqlConnection(cn.cadenaSql())) 
            {
                using (var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

    }
}
