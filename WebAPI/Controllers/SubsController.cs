using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SubsController : ApiController
    {
        private EventsEntities db = new EventsEntities();

        // GET: api/Subs
        public IQueryable<INSCRICAO> GetINSCRICAO()
        {
            return db.INSCRICAO;
        }

        // GET: api/Subs/5
        [ResponseType(typeof(INSCRICAO))]
        public IHttpActionResult GetINSCRICAO(int id)
        {
            INSCRICAO iNSCRICAO = db.INSCRICAO.Find(id);
            if (iNSCRICAO == null)
            {
                return NotFound();
            }

            return Ok(iNSCRICAO);
        }

        // PUT: api/Subs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutINSCRICAO(int id, INSCRICAO iNSCRICAO)
        {
            if (id != iNSCRICAO.COD_INSCRICAO)
            {
                return BadRequest();
            }

            db.Entry(iNSCRICAO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INSCRICAOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Subs
        [ResponseType(typeof(INSCRICAO))]
        public IHttpActionResult PostINSCRICAO(INSCRICAO iNSCRICAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            iNSCRICAO.FEEDBACK = new HashSet<FEEDBACK>();
            iNSCRICAO.DATA_HORA_INSC = DateTime.Now;
            iNSCRICAO.DATA_HORA_PARTICIPACAO = null;
            iNSCRICAO.COD_VALIDADO = 0;

            db.INSCRICAO.Add(iNSCRICAO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iNSCRICAO.COD_INSCRICAO }, iNSCRICAO);
        }

        // DELETE: api/Subs/5
        [ResponseType(typeof(INSCRICAO))]
        public IHttpActionResult DeleteINSCRICAO(int id)
        {
            INSCRICAO iNSCRICAO = db.INSCRICAO.Find(id);
            if (iNSCRICAO == null)
            {
                return NotFound();
            }

            db.INSCRICAO.Remove(iNSCRICAO);
            db.SaveChanges();

            return Ok(iNSCRICAO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool INSCRICAOExists(int id)
        {
            return db.INSCRICAO.Count(e => e.COD_INSCRICAO == id) > 0;
        }
    }
}