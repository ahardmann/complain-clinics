using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Clinic.Models;

namespace Clinic.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Clinic.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Ocorrencia>("OcorrenciasApi");
    builder.EntitySet<Endereco>("Endereco"); 
    builder.EntitySet<ApplicationUser>("ApplicationUsers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OcorrenciasApiController : ODataController
    {
        private ModeloDados db = new ModeloDados();

        // GET: odata/OcorrenciasApi
        [EnableQuery]
        public IQueryable<Ocorrencia> GetOcorrenciasApi()
        {
            return db.Ocorrencia;
        }

        // GET: odata/OcorrenciasApi(5)
        [EnableQuery]
        public SingleResult<Ocorrencia> GetOcorrencia([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ocorrencia.Where(ocorrencia => ocorrencia.ID_OCORRENCIA == key));
        }

        // PUT: odata/OcorrenciasApi(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Ocorrencia> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ocorrencia ocorrencia = db.Ocorrencia.Find(key);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            patch.Put(ocorrencia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcorrenciaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ocorrencia);
        }

        // POST: odata/OcorrenciasApi
        public IHttpActionResult Post(Ocorrencia ocorrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ocorrencia.Add(ocorrencia);
            db.SaveChanges();

            return Created(ocorrencia);
        }

        // PATCH: odata/OcorrenciasApi(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Ocorrencia> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ocorrencia ocorrencia = db.Ocorrencia.Find(key);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            patch.Patch(ocorrencia);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OcorrenciaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ocorrencia);
        }

        // DELETE: odata/OcorrenciasApi(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Ocorrencia ocorrencia = db.Ocorrencia.Find(key);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            db.Ocorrencia.Remove(ocorrencia);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/OcorrenciasApi(5)/Endereco
        [EnableQuery]
        public SingleResult<Endereco> GetEndereco([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ocorrencia.Where(m => m.ID_OCORRENCIA == key).Select(m => m.Endereco));
        }

        // GET: odata/OcorrenciasApi(5)/Usuario
        [EnableQuery]
        public SingleResult<ApplicationUser> GetUsuario([FromODataUri] int key)
        {
            return SingleResult.Create(db.Ocorrencia.Where(m => m.ID_OCORRENCIA == key).Select(m => m.Usuario));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OcorrenciaExists(int key)
        {
            return db.Ocorrencia.Count(e => e.ID_OCORRENCIA == key) > 0;
        }
    }
}
