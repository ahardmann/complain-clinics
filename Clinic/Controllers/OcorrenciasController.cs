using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Clinic.Models;
using Clinic.ViewModel;
using Microsoft.AspNet.Identity;

namespace Clinic.Controllers
{
    public class OcorrenciasController : Controller
    {
        private ModeloDados db = new ModeloDados();

        // GET: Ocorrencias
        public async Task<ActionResult> Index()
        {
            return View(await db.Ocorrencia.ToListAsync());
        }

        // GET: Ocorrencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencias ocorrencia = db.Ocorrencia.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencias/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ocorrencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NOME_CLINICA,NOME_MEDICO,SITE_CLINICA,ATRASO_MEDIO,NUMERO_OCORRENCIAS,DATA,Endereco")] OcorrenciasViewModel ocorrenciaVM)
        {
            if (ModelState.IsValid)
            {
                Ocorrencias ocorrencia = new Ocorrencias()
                {
                    Endereco = new Endereco()
                    {
                        BAIRRO = ocorrenciaVM.Endereco.BAIRRO,
                        CEP = ocorrenciaVM.Endereco.CEP,
                        CIDADE = ocorrenciaVM.Endereco.CIDADE,
                        ESTADO = ocorrenciaVM.Endereco.ESTADO,
                        RUA = ocorrenciaVM.Endereco.RUA,
                        TELEFONE_PRIMARIO = ocorrenciaVM.Endereco.TELEFONE_PRIMARIO,
                        TELEFONE_SECUNDARIO = ocorrenciaVM.Endereco.TELEFONE_SECUNDARIO
                    },
                    NOME_CLINICA = ocorrenciaVM.NOME_CLINICA,
                    NOME_MEDICO = ocorrenciaVM.NOME_MEDICO,
                    SITE_CLINICA = ocorrenciaVM.SITE_CLINICA,
                    ATRASO_MEDIO = ocorrenciaVM.ATRASO_MEDIO,
                    NUMERO_OCORRENCIAS = ocorrenciaVM.NUMERO_OCORRENCIAS,
                    DATA = ocorrenciaVM.DATA,
                    ID_USER = User.Identity.GetUserId()
                };
                db.Ocorrencia.Add(ocorrencia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ocorrenciaVM);
        }

        // GET: Ocorrencias/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencias ocorrencia = db.Ocorrencia.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId().Equals(ocorrencia.ID_USER))
            {
                return View(ocorrencia);
            }
            return RedirectToAction("Index");
        }

        // POST: Ocorrencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_OCORRENCIA,NOME_CLINICA,NOME_MEDICO,SITE_CLINICA,ATRASO_MEDIO,NUMERO_OCORRENCIAS,DATA,ENDERECO,ID_USER")] Ocorrencias ocorrenciaVM)
        {
            if (ModelState.IsValid)
            {
                Ocorrencias ocorrencia = db.Ocorrencia.Find(ocorrenciaVM.ID_OCORRENCIA);
                if (User.Identity.GetUserId().Equals(ocorrencia.ID_USER))
                {
                    ocorrencia.NOME_CLINICA = ocorrenciaVM.NOME_CLINICA;
                    ocorrencia.NOME_MEDICO = ocorrenciaVM.NOME_MEDICO;
                    ocorrencia.SITE_CLINICA = ocorrenciaVM.SITE_CLINICA;
                    ocorrencia.ATRASO_MEDIO = ocorrenciaVM.ATRASO_MEDIO;
                    ocorrencia.NUMERO_OCORRENCIAS = ocorrenciaVM.NUMERO_OCORRENCIAS;
                    ocorrencia.DATA = ocorrenciaVM.DATA;
                    ocorrencia.Endereco = ocorrenciaVM.Endereco;
                    ocorrencia.Endereco.ID_END = ocorrencia.ID_END;
                    db.Entry(ocorrencia).State = EntityState.Modified;
                    db.Entry(ocorrencia.Endereco).State = EntityState.Modified;
                    await db.SaveChangesAsync();                    
                }
                return RedirectToAction("Index");
            }
            return View(ocorrenciaVM);
        }

        // GET: Ocorrencias/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencias ocorrencia = db.Ocorrencia.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId().Equals(ocorrencia.ID_USER))
            {
                return View(ocorrencia);
            }
            return RedirectToAction("Index");
        }

        // POST: Ocorrencias/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocorrencias ocorrencia = db.Ocorrencia.Find(id);
            if (ocorrencia != null && User.Identity.GetUserId().Equals(ocorrencia.ID_USER))
            {
                db.Ocorrencia.Remove(ocorrencia);
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
