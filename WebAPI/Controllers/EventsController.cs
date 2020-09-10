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
    public class EventsController : ApiController
    {
        private EventsEntities db = new EventsEntities();

        // GET: api/Events
        public IQueryable<EVENTO> GetEVENTO()
        {
            return db.EVENTO;
        }

        // GET: api/Events/5
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult GetEVENTO(int id)
        {
            EVENTO eVENTO = db.EVENTO.Find(id);
            if (eVENTO == null)
            {
                return NotFound();
            }

            return Ok(eVENTO);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEVENTO(int id, EVENTO eVENTO)
        {
            if (id != eVENTO.COD_EVENTO)
            {
                return BadRequest();
            }

            db.Entry(eVENTO).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EVENTOExists(id))
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

        // POST: api/Events
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult PostEVENTO(EVENTO eVENTO)
        {
            db.EVENTO.Add(eVENTO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eVENTO.COD_EVENTO }, eVENTO);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(EVENTO))]
        public IHttpActionResult DeleteEVENTO(int id)
        {
            EVENTO eVENTO = db.EVENTO.Find(id);
            if (eVENTO == null)
            {
                return NotFound();
            }

            db.EVENTO.Remove(eVENTO);
            db.SaveChanges();

            return Ok(eVENTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EVENTOExists(int id)
        {
            return db.EVENTO.Count(e => e.COD_EVENTO == id) > 0;
        }
    }
}