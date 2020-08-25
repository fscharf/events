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
    public class UsersController : ApiController
    {
        private EventsEntities db = new EventsEntities();

        // GET: api/Users
        public IQueryable<USUARIO> GetUSUARIOs()
        {
            return db.USUARIOs;
        }

        // GET: api/Users/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(int id)
        {
            USUARIO user = db.USUARIOs.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUSUARIO(int id, USUARIO user)
        {
            if (id != user.COD_USUARIO)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!USUARIOExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult PostUSUARIO(USUARIO user)
        {
            if (ModelState.IsValid)
            {
                user.SENHA = Encrypt.GetHash(user.SENHA);
                db.USUARIOs.Add(user);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.COD_USUARIO }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult DeleteUSUARIO(int id)
        {
            USUARIO user = db.USUARIOs.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.USUARIOs.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USUARIOExists(int id)
        {
            return db.USUARIOs.Count(e => e.COD_USUARIO == id) > 0;
        }
    }
}