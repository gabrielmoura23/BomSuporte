using ProjetoBomNegocio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoBomNegocio.Controllers
{
    public class ContatoController : Controller
    {
        private DB_BomSuporteContext db2 = new DB_BomSuporteContext();

        // GET: Contato
        public ActionResult Index()
        {
            return View();
        }


        // POST: Tab_Suporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "nome,email,mensagem,data_cadastro, idusuario_cadastro")] ProjetoBomNegocio.Models.Tab_Contato tab_Contato)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db2.Contatos.Add(tab_Contato);
                    db2.SaveChanges();
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro. Favor tentar novamente.");
                    return View(tab_Contato);
                }

                TempData["Success"] = "Mensagem enviada com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Ocorreu um erro.");
            }

            ViewBag.Erro = "Erro ao tentar enviar mensagem. Favor tentar novamente!";
            return View(tab_Contato);
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