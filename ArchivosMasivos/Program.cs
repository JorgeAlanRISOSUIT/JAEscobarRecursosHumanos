using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace ArchivosMasivos
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "./../../../archivoMasivoEmpleado.txt";
            StreamReader reader = new StreamReader(path);
            if (File.Exists(path))
            {
                MasivoEmpleado(reader);
            }
            
            Console.ReadKey();
        }

        public static void MasivoDepartamento(StreamReader reader) {
            string linea = "";
            ML.Departamento departamento = new ML.Departamento();
            departamento.Departamentos = new List<ML.Departamento>();
            reader.ReadLine();
            while ((linea = reader.ReadLine()) != null)
            {
                string[] write = linea.Split('|');
                ML.Departamento lista = new ML.Departamento { Nombre = write[0] };
                departamento.Departamentos.Add(lista);
            }
            foreach (var objItem in departamento.Departamentos)
            {
                BL.Departamento.Add(objItem);
            }
        }

        public static void MasivoEmpleado(StreamReader reader)
        {
            string linea = "";
            ML.Empleado departamento = new ML.Empleado();
            departamento.Empleados = new List<ML.Empleado>();
            reader.ReadLine();
            while ((linea = reader.ReadLine()) != null)
            {
                string[] write = linea.Split('|');
                ML.Empleado lista = new ML.Empleado { 
                    NumeroEmpleado = write[0],
                    Nombre = write[1],
                    ApellidoPaterno = write[2],
                    ApellidoMaterno = write[3],
                    Email = write[4],
                    Genero = write[5],
                    FechaNacimiento = DateTime.Parse(write[6]),
                    Telefono = write[7],
                    Celular = write[8],
                    Sueldo = decimal.Parse(write[9]),
                    IdDepartamento = int.Parse(write[10]),
                };
                departamento.Empleados.Add(lista);
            }
            foreach (var objItem in departamento.Empleados)
            {
                BL.Empleado.Add(objItem);
            }
        }
    }
}
