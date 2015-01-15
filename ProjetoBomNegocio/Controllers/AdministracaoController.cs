using ProjetoBomNegocio.Contexto2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjetoBomNegocio.Controllers
{
    public class AdministracaoController : Controller
    {
        private DB_BomSuporteContext db2 = new DB_BomSuporteContext();

        // GET: Administracao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaSuporte(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Código" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var tab = db2.Suportes.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                tab = tab.Where(s => s.descricao.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Descrição":
                    tab = tab.OrderBy(s => s.descricao);
                    break;
                case "Slug":
                    tab = tab.OrderBy(s => s.descricao);
                    break;
                default:
                    tab = tab.OrderBy(s => s.descricao);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tab.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult _DetalhesSuporte(int id = 0)
        {
            ProjetoBomNegocio.Models.Tab_Suporte model = db2.Suportes.FirstOrDefault(t => t.idsuporte == id);
            return PartialView(model);
        }

        public ActionResult Cancelar2(int id = 0)
        {
            ProjetoBomNegocio.Models.Tab_Suporte model = db2.Suportes.FirstOrDefault(t => t.idsuporte == id);

            model.status = "Cancelado";
            model.data_alteracao = DateTime.Now;
            model.data_fechamento = DateTime.Now;
            model.idusuario_alteracao = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db2.Entry(model).State = EntityState.Modified;
                db2.SaveChanges();
                TempData["Success"] = "Cancelamento feito com sucesso!";
                //return RedirectToAction("Acompanhar");
            }
            else
            {
                TempData["Erro"] = "Erro ao cancelar. Favor tentar novamente!";
            }

            return RedirectToAction("ListaSuporte");
        }



        // POST: Tab_Suporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar([Bind(Include = "idsuporte,status,data_alteracao,idusuario_alteracao,descr_solucao")] ProjetoBomNegocio.Models.Tab_Suporte tab_Suporte)
        {
            var suporte = db2.Suportes.SingleOrDefault(t => t.idsuporte == tab_Suporte.idsuporte);
            if (suporte != null)
            {
                if (!suporte.status.ToUpper().Equals("ABERTO"))
                    ModelState.AddModelError(string.Empty, "É Necessário estar com suporte em aberto para alteração.");
                else
                {
                    try
                    {
                        suporte.descr_solucao = tab_Suporte.descr_solucao;
                        suporte.data_atendimento = tab_Suporte.data_alteracao;
                        suporte.data_alteracao = tab_Suporte.data_alteracao;
                        suporte.idusuario_alteracao = tab_Suporte.idusuario_alteracao;

                        db2.Entry(suporte).State = EntityState.Modified;
                        db2.SaveChanges();
                        TempData["Success"] = "feito com sucesso!";
                    }
                    catch
                    {
                        TempData["Erro"] = "Erro ao salvar. Favor tentar novamente!";
                        //ModelState.AddModelError(string.Empty, "Ocorreu um erro. Favor tentar novamente.");
                        //return View(suporte);
                    }
                }
            }
            else
            {
                TempData["Erro"] = "Erro. Favor tentar novamente!";
            }

            return RedirectToAction("ListaSuporte");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db2.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}