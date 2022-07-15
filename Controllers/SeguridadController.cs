using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using LoyalIGWEB.Models;
using Seguridad;

namespace LoyalIGWEB.Controllers
{
    public class SeguridadController : Controller
    {
        public Seguridad.Seguridad oSeguridad = new Seguridad.Seguridad();
        public Maestros.Consultas oConsultasMaestros = new Maestros.Consultas();
        // GET: Seguridad
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login oLogin)
        {
            try
            {
                if (oLogin.user.Trim() != "" && oLogin.password.Trim() != "")
                {
                    DataTable dtUsuario = oSeguridad.ValidarUsuario(oLogin.user, oLogin.password);

                    if (dtUsuario.Rows.Count > 0)
                    {
                        this.Session["_CodigoUsuario"] = Convert.ToInt32(dtUsuario.Rows[0]["CodigoUsuario"]);
                        this.Session["_NombreUsuario"] = dtUsuario.Rows[0]["NombreCompletoUsuario"].ToString();
                        this.Session["_NombreUsuarioPagina"] = dtUsuario.Rows[0]["NombrePagina"].ToString();
                        this.Session["_CodigoPerfil"] = Convert.ToInt32(dtUsuario.Rows[0]["CodigoPerfil"]);
                        this.Session["_NombrePerfil"] = dtUsuario.Rows[0]["NombrePerfil"].ToString();
                        this.Session["_CorreoUsuario"] = dtUsuario.Rows[0]["DireccionEmail"].ToString();


                        DataTable dtAgente = oConsultasMaestros.ObtenerAgenteUsuario(Convert.ToInt32(dtUsuario.Rows[0]["CodigoUsuario"]));
                        if (dtAgente.Rows.Count > 0)
                        {
                            this.Session["_CodigoAgente"] = dtAgente.Rows[0]["CodigoAgente"].ToString();
                            //this.Session["_NombreAgente"] = dtAgente.Rows[0]["NombreAgente"].ToString();
                            this.Session["_NombreAgente"] = "Demo";
                        }

                        return Redirect("/dashboard/principal");
                    }
                    else
                    { return View(oLogin); }
                }
                else
                { return View(oLogin); }
            }
            catch (Exception ex)
            {
                return Content("Ocurrio un Error : (" + ex.Message);
            }
        }
    }
}
