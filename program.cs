//Este es el código Original
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.EnterpriseServices.Internal;
using System.Configuration;

namespace iWokCore
{
    class Program
    {
        static string pathPlantillas = ConfigurationManager.AppSettings["PathPlantillas"];
        static string pathOutput = ConfigurationManager.AppSettings["PathOutput"];
       
        static void Main(string[] args)
        {
            try
            {
                switch (args.Length)
                {
                    case 0:
                        Console.WriteLine("Error en la invocación. \n la sintaxis correcta es: \n iWokCore.exe  nombreficheroExcel");
                        Console.ReadLine();
                        break;
                    case 1:
                        Generate(args[0]);
                        Console.ReadLine();
                        break;
                    case 3:
                        Generate(args[0],args[1],args[2]);
                        Console.ReadLine();
                        break;
                    case 4:
                        GenerateFromDdbb(args[2]);
                         Console.ReadLine();
                        break;
                    default:
                        break;

                }
               
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en tiempo de ejecución. \n Recuerde definir los paths de plantillas y salida en el fichero iWokCore.exe.config.\n Eror:" + ex.Message.ToString());
            }
        }

        private static void GeneratePopular()
        {
            Generate("PopularServiciosWeb.xlsx");
            Generate("PopularCustomDTOs.xlsx");
            Generate("PopularInvocables.xlsx");
            Generate("PopularFachadasYServicios.xlsx");
        }

        private static void Generate(string excel)
        {
            Generate(pathPlantillas, pathOutput, excel);
        }

        private static void Generate(string templates, string output, string excel)
        {
            MessagesColector.Instance.ClearWarnings();
            MessagesColector.Instance.SetUser("Test");
            IWokFactory.FromExcel(templates + excel, output);

            MessagesColector.Instance.NotififyGeneratedFiles();
            Console.WriteLine("Finalizando!");
        }

        private static void GenerateFromDdbb(string excel)
        {
            MessagesColector.Instance.ClearWarnings();
            MessagesColector.Instance.SetUser("Test");
            IWokFactory.FromDdbb(
                   pathPlantillas + excel,
                   pathOutput);

            MessagesColector.Instance.NotififyGeneratedFiles();
            Console.WriteLine("Finalizando!");
        }
    }
}