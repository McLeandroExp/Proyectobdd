
using Microsoft.Data.SqlClient;

class Program

{

    static void Main(string[] args)

    {

        bool salir = false;

        int id;

        string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\lourd\\source\\repos\\ConsoleApp2\\ConsoleApp2\\Database1.mdf; Integrated Security = True";

    

        while (!salir)

        {

            Console.WriteLine("\n\n---------------------------------------------------------------------------------------");

            Console.WriteLine("-------------------------------- Conexión Directa a BDD -------------------------------");

            Console.WriteLine("\n----------------------------    Bienvenidx a BDD Clientes    --------------------------");

            Console.WriteLine("\nElige una opción:" + "\n1. Insertar registro" + "\n2. Imprimir registros" +

                "\n3. Actualizar registro" + "\n4. Eliminar registro" + "\n5. Salir");



            try

            {

                int opcion = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n");



                switch (opcion)

                {

                    case 1:

                        Console.Write("Ingresa el nombre: ");

                        string name = Console.ReadLine();



                        using (SqlConnection connection = new SqlConnection(connectionString))

                        {

                            connection.Open();

                            using (SqlCommand command = new SqlCommand("INSERT INTO MiTabla (Nombre) VALUES (@Nombre)", connection))

                            {

                                command.Parameters.AddWithValue("@Nombre", name);

                                command.ExecuteNonQuery();

                            }

                        }



                        Console.WriteLine("Registro creado.");

                        break;



                    case 2:

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("SELECT * FROM MiTabla", connection))
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
                            connection.Close(); // Cerrar la conexión después de utilizarla
                        }
                        break;


                    case 3:

                        Console.Write("Ingresa el ID del registro a actualizar: ");

                        id = int.Parse(Console.ReadLine());



                        Console.Write("Ingresa el nuevo nombre: ");

                        string newName = Console.ReadLine();



                        using (SqlConnection connection = new SqlConnection(connectionString))

                        {

                            connection.Open();

                            using (SqlCommand command = new SqlCommand("UPDATE MiTabla SET Nombre = @NewName WHERE Id = @Id", connection))

                            {

                                command.Parameters.AddWithValue("@NewName", newName);

                                command.Parameters.AddWithValue("@Id", id);

                                command.ExecuteNonQuery();

                            }

                        }



                        Console.WriteLine("Registro actualizado.");

                        break;



                    case 4:

                        Console.Write("Ingresa el ID del registro a eliminar: ");

                        id = int.Parse(Console.ReadLine());



                        using (SqlConnection connection = new SqlConnection(connectionString))

                        {

                            connection.Open();

                            using (SqlCommand command = new SqlCommand("DELETE FROM MiTabla WHERE Id = @Id", connection))

                            {

                                command.Parameters.AddWithValue("@Id", id);

                                command.ExecuteNonQuery();

                            }

                        }



                        Console.WriteLine("Registro eliminado.");

                        break;



                    case 5:

                        salir = true;

                        Console.WriteLine("Has salido");

                        break;

                }

            }

            catch (FormatException)

            {

                Console.WriteLine("Ingresa una de las opciones del menú");

            }

        }

    }
}