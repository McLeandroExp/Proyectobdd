
using Microsoft.Data.SqlClient;
//using System;

namespace ConsoleApp2.Models
{
    internal class DataManaging
    {
        private string connectionString;
        private SqlConnection connection;

        public DataManaging(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new SqlConnection(connectionString);
        }

        public void InsertarRegistro()
        {
            int nCaracteres = 3;
            Console.Write("Ingresa el nombre: ");
            string name = Console.ReadLine();
            if (name?.Length <= nCaracteres) { Console.WriteLine($"El nombre debe de tener al menos {nCaracteres} caracteres"); return; }

            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO MiTabla (Nombre) VALUES (@Nombre)", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", name);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Registro creado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void MostrarRegistros()
        {
            int numeroRegistros = 10;
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT TOP {numeroRegistros} * FROM MiTabla;", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int recordId = reader.GetInt32(0);
                            string recordName = reader.GetString(1);
                            Console.WriteLine($"\t| ID: {recordId} | Nombre: {recordName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al mostrar los registros: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        public void ActualizarRegistro()
        {
            MostrarRegistros();

            Console.Write("Ingresa el ID del registro a actualizar: ");
            int id = int.Parse(Console.ReadLine());
           
            try
            {
                connection.Open();

                // Verificar la existencia del ID
                if (!ExisteRegistro(id))
                {
                    Console.WriteLine($"El ID {id} no existe.");
                    return;
                }
                Console.Write("Ingresa el nuevo nombre: ");
                string newName = Console.ReadLine();

                // Actualizar el registro
                using (SqlCommand command = new SqlCommand("UPDATE MiTabla SET Nombre = @NewName WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Registro actualizado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private bool ExisteRegistro(int id)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM MiTabla WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }


        public void EliminarRegistro()
        {
            MostrarRegistros();
            Console.Write("Ingresa el ID del registro a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                connection.Open();
                if (!ExisteRegistro(id))
                {
                    Console.WriteLine($"El ID {id} no existe.");
                    return;
                }
                using (SqlCommand command = new SqlCommand("DELETE FROM MiTabla WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Registro eliminado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
