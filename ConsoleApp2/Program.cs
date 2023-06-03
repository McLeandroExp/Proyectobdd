
using ConsoleApp2.Models;
using Microsoft.Data.SqlClient;

class Program

{

    static void Main(string[] args)

    {

        bool salir = false;


        string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\lourd\\source\\repos\\ConsoleApp2\\ConsoleApp2\\Database1.mdf; Integrated Security = True";

        DataManaging dataManaging = new DataManaging(connectionString);

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

                        dataManaging.InsertarRegistro();

                        break;



                    case 2:

                        dataManaging.MostrarRegistros();
                        
                        break;


                    case 3:

                        dataManaging.ActualizarRegistro();

                        break;



                    case 4:

                        dataManaging.EliminarRegistro();

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