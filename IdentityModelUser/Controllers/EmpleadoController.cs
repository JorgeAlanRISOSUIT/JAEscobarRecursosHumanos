using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityModelUser.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleados
        public ActionResult Empleados()
        {
            var result = BL.Empleado.GetAll();
            if (result.Item1)
            {
                ViewBag.Mensaje = false;
                return View(result.Item4);
            }
            else
            {
                ML.Empleado empleado = new ML.Empleado();
                ViewBag.Mensaje = true;
                return View(empleado);
            }
        }

        public ActionResult ObtenerEmpleado(int? id)
        {
            if (id.HasValue)
            {
                var result = BL.Empleado.GetById(id.Value);
                if (result.Item1)
                {
                    var resultDepartamento = BL.Departamento.GetAll();
                    result.Item4.Departamento.Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>();
                    return View(result.Item4);
                }
                else
                {
                    return RedirectToAction("Empleados");
                }
            }
            else
            {
                ML.Empleado nuevo = new ML.Empleado();
                var resultDepartamento = BL.Departamento.GetAll();
                nuevo.Departamento = new ML.Departamento { Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>() };
                return View(nuevo);
            }
        }

        [HttpPost]
        public ActionResult ObtenerEmpleado(ML.Empleado empleado)
        {
            if (empleado.IdEmpleado == 0)
            {
                var resultGetDepartamento = BL.Departamento.GetById(empleado.Departamento.IdDepartamento);
                empleado.Departamento = resultGetDepartamento.Item1 ? resultGetDepartamento.Item4 : new ML.Departamento();
                var result = BL.Empleado.Add(empleado);
                var resultDepartamento = BL.Departamento.GetAll();
                if (result.Item1)
                {
                    ML.Empleado nuevo = new ML.Empleado();
                    nuevo.Departamento = new ML.Departamento();
                    nuevo.Departamento.Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>();
                    return View(nuevo);
                }
                else
                {
                    empleado.Departamento.Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>();
                    return View(empleado);
                }
            }
            else
            {
                var resultGetDepartamento = BL.Departamento.GetById(empleado.Departamento.IdDepartamento);
                empleado.Departamento = resultGetDepartamento.Item1 ? resultGetDepartamento.Item4 : new ML.Departamento();
                var result = BL.Empleado.Update(empleado);
                var resultDepartamento = BL.Departamento.GetAll();
                if (result.Item1)
                {
                    empleado.Departamento.Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>();
                    return View(empleado);
                }
                else
                {
                    empleado.Departamento.Departamentos = resultDepartamento.Item1 ? resultDepartamento.Item4.Departamentos : new List<ML.Departamento>();
                    return View(empleado);
                }
            }
        }

        [HttpPost]
        public JsonResult EliminarEmpleado(int idEmpleado)
        {
            var result = BL.Empleado.Delete(idEmpleado);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}