﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Especialidades : System.Web.UI.Page
    {
        public List<Especialidad> ListaEspecialidades;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    cargarEspecialidades();

                }

                else if (Request.QueryString["action"].ToString() == "elim")
                {
                   bajaEspecialidad();
                }

                else if (Request.QueryString["action"].ToString() == "mod")
                {
                    modificarEspecialidad();
                } 

            }
            else
            {
               ListaEspecialidades = (List<Especialidad>)Session["ListaEspecialidades"];
            }
        }

        public void cargarEspecialidades()
        {
            EspecialidadesNegocio negocio = new EspecialidadesNegocio();
            try
            {
                labelModificar.Style["Visibility"] = "hidden";
                BtnModificar.Style["Visibility"] = "hidden";
                BtnCancelar.Style["Visibility"] = "hidden";
                ListaEspecialidades = negocio.Listar();
               
                Session.Add("ListaEspecialidades", ListaEspecialidades);

            }
            catch (Exception)
            {

                throw;
            }
        } 


        public void bajaEspecialidad()
        {
            EspecialidadesNegocio negocio = new EspecialidadesNegocio();
            int id = int.Parse(Request.QueryString["id"]);
            try
            {
                negocio.eliminarEspecialidad(id);
                Response.Redirect("Especialidades.aspx");
            }
            catch (Exception)
            {

                throw;
            } 
        } 

        public void modificarEspecialidad()
        {
            int id = int.Parse(Request.QueryString["id"]);
            BtnAgregar.Style["Visibility"] = "hidden";
            labelAlta.Style["Visibility"] = "hidden";
            BtnModificar.Style["Visibility"] = "visible";
            BtnCancelar.Style["Visibility"] = "visible";
            labelModificar.Style["Visibility"] = "visible";
            ListaEspecialidades = (List<Especialidad>)Session["ListaEspecialidades"];
            Especialidad aModificar = ListaEspecialidades.Find(x => x.ID == id);
            TextBoxNombre.Text = aModificar.Nombre;
            Session.Add("idModificar", id); 
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            EspecialidadesNegocio negocio = new EspecialidadesNegocio();
            Especialidad aux = new Especialidad();

            aux.Nombre=TextBoxNombre.Text;
            negocio.Agregar(aux);
            Response.Redirect("Especialidades.aspx");
        } 

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            EspecialidadesNegocio negocio = new EspecialidadesNegocio();
            Especialidad aux = new Especialidad((int)Session["idModificar"], TextBoxNombre.Text);

            try
            {
                negocio.modificar(aux);
                Response.Redirect("Especialidades.aspx");

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Especialidades.aspx"); 
        }
    }
}