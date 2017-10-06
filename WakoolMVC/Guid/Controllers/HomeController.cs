using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MetadataDiscover;
using Guid.Web;
using BusinessLogicalLayer;
using System.IO;

namespace Guid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string entity = " ")
        {
            Type type = null;
            Type[] types = AssemblyUtils.GetEntities().ToArray();
            if (!string.IsNullOrWhiteSpace(entity))
                foreach (Type t in types)
                {
                    if (AssemblyUtils.GetEntityDisplayAttribute(t).Trim().ToUpper() == entity.Replace("Entity.", "").Trim().ToUpper())
                    {
                        type = t;
                    }
                }

            return View(type);
        }
        [HttpPost]
        public ActionResult Index([ModelBinder(typeof(EntityModelBinder))]object obj)
        {
            new EntityBLL().Insert((EntityBase)obj);
            string text = AssemblyUtils.GetEntityDisplayAttribute(obj.GetType());
            return RedirectToAction("index", new { entity = text });
        }

        public ActionResult edit(int id, string entity)
        {
            Type type = null;

            Type[] types = AssemblyUtils.GetEntities().ToArray();
            if (entity != null)
                foreach (Type t in types)
                {

                    if (t.Name.Replace("Entity.", "").Trim().ToUpper() == entity.Replace("Entity.", "").Trim().ToUpper())
                    {
                        type = t;
                    }
                }

            object p = Activator.CreateInstance(type);
            p.GetType().GetProperty("ID").SetValue(p, id);
            object objEdit = new EntityBLL().GetById(((EntityBase)p), type);
            return View(objEdit);
        }

        public FileResult ReportWebMaker(string entity, string tipo)
        {
            
            Type type = AssemblyUtils.GetEntityByName(entity.Replace("Entity.", "").Trim().ToUpper());
            object obj = Activator.CreateInstance(type);
            List<object> listItem = new EntityBLL().GetAll(((EntityBase)obj), type);
            //if(listItem.Count > 0)
               // if(((EntityBase)(listItem[0])).ID >0)
           
            if (!System.IO.Directory.Exists(@"C:\Users\Public\Reporty"))
                System.IO.Directory.CreateDirectory(@"C:\Users\Public\Reporty");

            string nome = @"C:\Users\Public\Reporty\" + entity.Trim().ToUpper() + DateTime.Now.ToString().Replace(" ", "").Replace("/", "").Replace(":", "") + ((tipo.ToUpper().Contains("EXCEL")) ? ".xls" : ".pdf");
            if (tipo.ToUpper().Contains("EXCEL"))
                ExcelCreator.ExcelCreation.ListToExcel(listItem, nome, type);
            else
                PDFCreator.PDFCreation.BuildPDF(listItem, nome, type);
           
                byte[] fileBytes = System.IO.File.ReadAllBytes(nome);

                System.IO.File.Delete(nome);
                if (tipo.Contains("EXCEL"))
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, entity.Trim().ToUpper() + ".xls");
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, entity.Trim().ToUpper() + ".pdf");
            
            //return RedirectToAction("index", new { entity = entity }); ;
        }

        [HttpPost]
        public ActionResult edit([ModelBinder(typeof(EntityModelBinder))]object obj)
        {
            new EntityBLL().Insert((EntityBase)obj);
            string text = AssemblyUtils.GetEntityDisplayAttribute(obj.GetType());
            return RedirectToAction("index", new { entity = text });
        }

        public ActionResult delite(int id, string entity)
        {
            Type type = null;
            Type[] types = AssemblyUtils.GetEntities().ToArray();
            if (entity != null)
                foreach (Type t in types)
                {
                    if (t.Name.Replace("Entity.", "").Trim().ToUpper() == entity.Replace("Entity.", "").Trim().ToUpper())
                    {
                        type = t;
                    }
                }

            object p = Activator.CreateInstance(type);
            p.GetType().GetProperty("ID").SetValue(p, id);
            object objEdit = new EntityBLL().GetById(((EntityBase)p), type);
            return View(objEdit);
        }

        [HttpPost]
        public ActionResult delite(string entity, int id)
        {
            Type type = null;
            Type[] types = AssemblyUtils.GetEntities().ToArray();
            if (entity != null)
                foreach (Type t in types)
                {
                    if (t.Name.Replace("Entity.", "").Trim().ToUpper() == entity.Replace("Entity.", "").Trim().ToUpper())
                    {
                        type = t;
                    }
                }

            object obj = Activator.CreateInstance(type);
            obj.GetType().GetProperty("ID").SetValue(obj, id);
            new EntityBLL().Delete((EntityBase)obj, obj.GetType());
            string text = AssemblyUtils.GetEntityDisplayAttribute(obj.GetType());
            return RedirectToAction("index", new { entity = text });
        }
    }
}