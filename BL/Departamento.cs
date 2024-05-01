using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static (bool, string, Exception, ML.Departamento) GetAll()
        {
            ML.Departamento resultDepto = new ML.Departamento();
            resultDepto.Departamentos = new List<ML.Departamento>();
            try
            {
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    var result = context.Departamentoes.ToList();
                    if (result.Count > 0)
                    {
                        foreach (var objResult in result)
                        {
                            ML.Departamento objDepto = new ML.Departamento
                            {
                                IdDepartamento = objResult.IdDepartamento,
                                Nombre = objResult.Nombre
                            };
                            resultDepto.Departamentos.Add(objDepto);
                        }
                        return (true, string.Empty, null, resultDepto);
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

        public static (bool, string, Exception, ML.Departamento) GetById(int idDepartamento)
        {
            try
            {
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    var result = context.Departamentoes.SingleOrDefault(value => value.IdDepartamento == idDepartamento);
                    if(result != null)
                    {
                        ML.Departamento objDepartamento = new ML.Departamento
                        {
                            IdDepartamento = result.IdDepartamento,
                            Nombre = result.Nombre
                        };
                        return (true, "", null, objDepartamento);
                    }
                    else
                    {
                        return (false, "No se encuentra el departamento", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
        
        public static (bool, string, Exception) Add(ML.Departamento depto)
        {
            try
            {
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    DL.Departamento nuevo = new DL.Departamento { Nombre = depto.Nombre };
                    context.Departamentoes.Add(nuevo);
                    int row = context.SaveChanges();
                    if (row > 0)
                    {
                        return (true, "", null);
                    }
                    else
                    {
                        return (false, "No se inserto el departamento", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }
        
        public static (bool, string, Exception) Delete(int idDepartamento)
        {
            try
            {
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    var result = context.Departamentoes.SingleOrDefault(value => value.IdDepartamento == idDepartamento);
                    if (result != null)
                    {
                        context.Departamentoes.Remove(result);
                        int row = context.SaveChanges();
                        if (row > 0)
                        {
                            return (true, "", null);
                        }
                        else
                        {
                            return (false, "No se ha eliminado el departamento", null);
                        }
                    }
                    else
                    {
                        return (false, "No se encuentra el departamento", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex);
            }
        }

        public static (bool, string, Exception) Update(ML.Departamento departamento)
        {
            try
            {
                using (DL.ModelIdentity context = new DL.ModelIdentity())
                {
                    var result = context.Departamentoes.SingleOrDefault(value => value.IdDepartamento == departamento.IdDepartamento);
                    if (result != null)
                    {
                        result.Nombre = departamento.Nombre;
                        int row = context.SaveChanges();
                        if(row > 0)
                        {
                            return (true, "", null);
                        }
                        else
                        {
                            return (false, "No se actualizo el departamento", null);
                        }
                    }
                    else
                    {
                        return (false, "No se encuentra el departamento", null);
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
