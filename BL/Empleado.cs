using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static (bool, string, Exception, ML.Empleado) GetAll()
        {
            try
            {
                ML.Empleado resultEmp = new ML.Empleado { Empleados = new List<ML.Empleado>() };
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    var result = (from Empleado in context.Empleadoes
                                  join Departamento in context.Departamentoes on Empleado.IdDepartamento equals Departamento.IdDepartamento into DeptoEmp
                                  from g1 in DeptoEmp.DefaultIfEmpty()
                                  select new
                                  {
                                      Empleado.IdEmpleado,
                                      Empleado.Nombre,
                                      Empleado.ApellidoPaterno,
                                      Empleado.ApellidoMaterno,
                                      Empleado.FechaNacimiento,
                                      Empleado.Email,
                                      Empleado.Genero,
                                      Empleado.NumeroEmpleado,
                                      Empleado.Telefono,
                                      Empleado.Celular,
                                      Empleado.Sueldo,
                                      g1.IdDepartamento,
                                      NombreDepartamento = g1.Nombre
                                  }).ToList();
                    if (result.Count > 0)
                    {
                        foreach (var objResult in result)
                        {
                            ML.Empleado empleado = new ML.Empleado
                            {
                                IdEmpleado = objResult.IdEmpleado,
                                Nombre = objResult.Nombre,
                                NumeroEmpleado = objResult.NumeroEmpleado,
                                ApellidoPaterno = objResult.ApellidoPaterno,
                                ApellidoMaterno = objResult.ApellidoMaterno,
                                Email = objResult.Email,
                                Genero = objResult.Genero,
                                FechaNacimiento = objResult.FechaNacimiento,
                                Telefono = objResult.Telefono,
                                Celular = objResult.Celular,
                                Sueldo = objResult.Sueldo,
                                IdDepartamento = objResult.IdDepartamento,
                                Departamento = new ML.Departamento
                                {
                                    IdDepartamento = objResult.IdDepartamento,
                                    Nombre = objResult.NombreDepartamento
                                }
                            };
                            resultEmp.Empleados.Add(empleado);
                        }
                        return (true, "", null, resultEmp);
                    }
                    else
                    {
                        return (false, "", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception, ML.Empleado) GetById(int idEmpleado)
        {
            try
            {
                using (DL.JAEscobarRecursosHumanosEntities context = new DL.JAEscobarRecursosHumanosEntities())
                {
                    var result = (from Empleado in context.Empleadoes
                                  join Departamento in context.Departamentoes on Empleado.IdEmpleado equals Departamento.IdDepartamento into DeptoEmp
                                  from g1 in DeptoEmp
                                  select new
                                  {
                                      Empleado.IdEmpleado,
                                      Empleado.Nombre,
                                      Empleado.ApellidoPaterno,
                                      Empleado.ApellidoMaterno,
                                      Empleado.FechaNacimiento,
                                      Empleado.Genero,
                                      Empleado.NumeroEmpleado,
                                      Empleado.Telefono,
                                      Empleado.Celular,
                                      Empleado.Sueldo,
                                      Departamentos = new DL.Departamento
                                      {
                                          IdDepartamento = g1.IdDepartamento,
                                          Nombre = g1.Nombre,
                                      }
                                  }).SingleOrDefault(value => value.IdEmpleado == idEmpleado);
                    if (result != null)
                    {
                        ML.Empleado empleado = new ML.Empleado
                        {
                            IdEmpleado = result.IdEmpleado,
                            Nombre = result.Nombre,
                            ApellidoPaterno = result.ApellidoPaterno,
                            ApellidoMaterno = result.ApellidoMaterno,
                            FechaNacimiento = result.FechaNacimiento,
                            Genero = result.Genero,
                            NumeroEmpleado = result.NumeroEmpleado,
                            Telefono = result.Telefono,
                            Celular = result.Celular,
                            Sueldo = result.Sueldo,
                            Departamento = new ML.Departamento
                            {
                                IdDepartamento = result.Departamentos.IdDepartamento,
                                Nombre = result.Departamentos.Nombre
                            }
                        };
                        return (true, "", null, empleado);
                    }
                    else
                    {
                        return (false, "Empleado no registrado", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }

        public static (bool, string, Exception) Add(ML.Empleado empleado)
        {
            try
            {
                using (DL.JAEscobarRecursosHumanosEntities context = new DL.JAEscobarRecursosHumanosEntities())
                {
                    DL.Empleado objEmpleado = new DL.Empleado
                    {
                        Nombre = empleado.Nombre,
                        ApellidoPaterno = empleado.ApellidoPaterno,
                        ApellidoMaterno = empleado.ApellidoMaterno,
                        FechaNacimiento = empleado.FechaNacimiento,
                        Genero = empleado.Genero,
                        NumeroEmpleado = empleado.NumeroEmpleado,
                        Email = empleado.Email,
                        Telefono = empleado.Telefono,
                        Celular = empleado.Celular,
                        Sueldo = empleado.Sueldo.GetValueOrDefault(0),
                        IdDepartamento = empleado.Departamento.IdDepartamento,
                    };
                    context.Empleadoes.Add(objEmpleado);
                    int row = context.SaveChanges();
                    if (row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se agrego el empleado", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) Delete(int idEmpleado)
        {
            try
            {
                using (DL.JAEscobarRecursosHumanosEntities context = new DL.JAEscobarRecursosHumanosEntities())
                {
                    var result = context.Empleadoes.SingleOrDefault(value => value.IdEmpleado == idEmpleado);
                    if (result != null)
                    {
                        context.Empleadoes.Remove(result);
                        int row = context.SaveChanges();
                        if (row > 0)
                        {
                            return (true, "", null);
                        }
                        else
                        {
                            return (false, "No se eliminó el empleado", null);
                        }
                    }
                    else
                    {
                        return (false, "El empleado no se encuentra registrado", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) Update(ML.Empleado empleado)
        {
            try
            {
                using (DL.JAEscobarRecursosHumanosEntities context = new DL.JAEscobarRecursosHumanosEntities())
                {
                    var result = context.Empleadoes.SingleOrDefault(value => value.IdEmpleado == empleado.IdEmpleado);
                    if (result != null)
                    {
                        result.Nombre = empleado.Nombre;
                        result.ApellidoPaterno = empleado.ApellidoPaterno;
                        result.ApellidoMaterno = empleado.ApellidoMaterno;
                        result.FechaNacimiento = empleado.FechaNacimiento;
                        result.Genero = empleado.Genero;
                        result.Email = empleado.Email;
                        result.NumeroEmpleado = empleado.NumeroEmpleado;
                        result.Telefono = empleado.Telefono;
                        result.Celular = empleado.Celular;
                        result.Sueldo = empleado.Sueldo.GetValueOrDefault(0);
                        result.IdDepartamento = empleado.Departamento.IdDepartamento;
                        int row = context.SaveChanges();
                        if (row > 0)
                        {
                            return (true, "", null);
                        }
                        else
                        {
                            return (false, "", null);
                        }
                    }
                    else
                    {
                        return (false, "El empleado no se encuentra registrado", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
    }
}
