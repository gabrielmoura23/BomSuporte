using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoBomNegocio.Models;

namespace ProjetoBomNegocio.Controllers
{
    public class Tab_SuporteController : Controller
    {
        private DB_BomSuporteContext db = new DB_BomSuporteContext();

        // GET: Tab_Suporte
        public ActionResult Index()
        {
            return View(db.Suportes.ToList());
        }

        // GET: Tab_Suporte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tab_Suporte tab_Suporte = db.Suportes.Find(id);
            if (tab_Suporte == null)
            {
                return HttpNotFound();
            }
            return View(tab_Suporte);
        }

        // GET: Tab_Suporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tab_Suporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idsuporte,descricao,sistema_operacional,problema_recorrente,prioridade,email,ddd_telefone,telefone,melhor_horario,status,flg_termo_aceito,data_abertura,idusuario_cadastro,data_alteracao,idusuario_alteracao,data_atendimento,data_fechamento,descr_solucao,valor_contribuicao")] Tab_Suporte tab_Suporte)
        {
            if (ModelState.IsValid)
            {
                db.Suportes.Add(tab_Suporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tab_Suporte);
        }

        // GET: Tab_Suporte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tab_Suporte tab_Suporte = db.Suportes.Find(id);
            if (tab_Suporte == null)
            {
                return HttpNotFound();
            }
            return View(tab_Suporte);
        }

        // POST: Tab_Suporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idsuporte,descricao,sistema_operacional,problema_recorrente,prioridade,email,ddd_telefone,telefone,melhor_horario,status,flg_termo_aceito,data_abertura,idusuario_cadastro,data_alteracao,idusuario_alteracao,data_atendimento,data_fechamento,descr_solucao,valor_contribuicao")] Tab_Suporte tab_Suporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tab_Suporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tab_Suporte);
        }

        // GET: Tab_Suporte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tab_Suporte tab_Suporte = db.Suportes.Find(id);
            if (tab_Suporte == null)
            {
                return HttpNotFound();
            }
            return View(tab_Suporte);
        }

        // POST: Tab_Suporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tab_Suporte tab_Suporte = db.Suportes.Find(id);
            db.Suportes.Remove(tab_Suporte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
