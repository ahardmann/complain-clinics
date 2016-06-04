using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Doando.Models;
using Doando.ViewModel;
using Microsoft.AspNet.Identity;

namespace Doando.Controllers
{
    public class OngController : Controller
    {
        private ModeloDados db = new ModeloDados();

        // GET: Ong
        public async Task<ActionResult> Index()
        {
            return View(await db.Ong.ToListAsync());
        }

        // GET: Ong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ong ong = db.Ong.Find(id);
            if (ong == null)
            {
                return HttpNotFound();
            }
            return View(ong);
        }

        // GET: Ong/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CNPJ,NOME,SITE,EMAIL,Endereco")] OngViewModel ongVM)
        {
            if (ModelState.IsValid)
            {
                Ong ong = new Ong()
                {
                    Endereco = new Endereco()
                    {
                        BAIRRO = ongVM.Endereco.BAIRRO,
                        CEP = ongVM.Endereco.CEP,
                        CIDADE = ongVM.Endereco.CIDADE,
                        ESTADO = ongVM.Endereco.ESTADO,
                        RUA = ongVM.Endereco.RUA,
                        TELEFONE_PRIMARIO = ongVM.Endereco.TELEFONE_PRIMARIO,
                        TELEFONE_SECUNDARIO = ongVM.Endereco.TELEFONE_SECUNDARIO
                    },
                    CNPJ = ongVM.CNPJ,
                    SITE = ongVM.SITE,
                    NOME = ongVM.NOME,
                    EMAIL = ongVM.EMAIL,
                    ID_USER = User.Identity.GetUserId()
                };
                db.Ong.Add(ong);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ongVM);
        }

        // GET: Ong/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ong ong = db.Ong.Find(id);
            if (ong == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId().Equals(ong.ID_USER))
            {
                return View(ong);
            }
            return RedirectToAction("Index");
        }

        // POST: Ong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_ONG,CNPJ,NOME,SITE,EMAIL,ENDERECO,ID_USER")] Ong ongVM)
        {
            if (ModelState.IsValid)
            {
                Ong ong = db.Ong.Find(ongVM.ID_ONG);
                if (User.Identity.GetUserId().Equals(ong.ID_USER))
                {
                    ong.NOME = ongVM.NOME;
                    ong.SITE = ong.SITE;
                    ong.CNPJ = ongVM.CNPJ;
                    ong.EMAIL = ongVM.EMAIL;
                    ong.Endereco = ongVM.Endereco;
                    ong.Endereco.ID_END = ong.ID_END;
                    db.Entry(ong).State = EntityState.Modified;
                    db.Entry(ong.Endereco).State = EntityState.Modified;
                    await db.SaveChangesAsync();                    
                }
                return RedirectToAction("Index");
            }
            return View(ongVM);
        }

        // GET: Ong/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ong ong = db.Ong.Find(id);
            if (ong == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId().Equals(ong.ID_USER))
            {
                return View(ong);
            }
            return RedirectToAction("Index");
        }

        // POST: Ong/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ong ong = db.Ong.Find(id);
            if (ong != null && User.Identity.GetUserId().Equals(ong.ID_USER))
            {
                db.Ong.Remove(ong);
                db.SaveChangesAsync();
            }
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
