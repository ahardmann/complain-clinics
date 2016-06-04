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
using Microsoft.AspNet.Identity;

namespace Doando.Controllers
{
    public class NecessidadeController : Controller
    {
        private ModeloDados db = new ModeloDados();

        // GET: Necessidade
        public async Task<ActionResult> Index()
        {
            return View(await db.Necessidade.ToListAsync());
        }

        // GET: Necessidade/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necessidade necessidade = await db.Necessidade.FindAsync(id);
            if (necessidade == null)
            {
                return HttpNotFound();
            }
            return View(necessidade);
        }

        // GET: Necessidade/Create
        [Authorize]
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.ID_ONG = new SelectList
               (
                    db.Ong.Where(ong => ong.ID_USER == userId).ToList(),
                   "ID_ONG",
                   "NOME"
               );

            return View();
        }

        // POST: Necessidade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_NECESSIDADE,DESCRICAO,TITULO,PRIORIDADE,DATA,ID_ONG")] Necessidade necessidade)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                if (db.Ong.Any(o => o.ID_USER == userId))
                {
                    db.Necessidade.Add(necessidade);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            return View(necessidade);
        }

        // GET: Necessidade/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necessidade necessidade = await db.Necessidade.FindAsync(id);
            if (necessidade == null)
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();
            if (necessidade.Ong.ID_USER.Equals(userId))
            {
                ViewBag.ID_ONG = new SelectList
                   (
                        db.Ong.Where(ong => ong.ID_USER == userId).ToList(),
                       "ID_ONG",
                       "NOME",
                       necessidade.ID_ONG
                   );
                return View(necessidade);
            }
            return RedirectToAction("Index");
        }

        // POST: Necessidade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_NECESSIDADE,DESCRICAO,TITULO,PRIORIDADE,DATA,ID_ONG")] Necessidade necessidadeVM)
        {
            if (ModelState.IsValid)
            {
                Necessidade necessidade = db.Necessidade.Find(necessidadeVM.ID_NECESSIDADE);
                if (User.Identity.GetUserId().Equals(necessidade.Ong.ID_USER))
                {
                    necessidade.PRIORIDADE = necessidadeVM.PRIORIDADE;
                    necessidade.TITULO = necessidadeVM.TITULO;
                    necessidade.DESCRICAO = necessidadeVM.DESCRICAO;
                    necessidade.DATA = necessidadeVM.DATA;                    
                    db.Entry(necessidade).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(necessidadeVM);
        }

        // GET: Necessidade/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Necessidade necessidade = await db.Necessidade.FindAsync(id);
            if (necessidade == null)
            {
                return HttpNotFound();
            }
            if (necessidade.Ong.ID_USER.Equals(User.Identity.GetUserId()))
            {
                return View(necessidade);
            }
            return RedirectToAction("Index");
        }

        // POST: Necessidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Necessidade necessidade = await db.Necessidade.FindAsync(id);
            if (necessidade.Ong.ID_USER.Equals(User.Identity.GetUserId()))
            {
                db.Necessidade.Remove(necessidade);
                await db.SaveChangesAsync();
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
