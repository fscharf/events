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
        private readonly EventsEntities db = new EventsEntities();

        // GET: api/Users
        public IQueryable<USUARIO> GetUSUARIO()
        {
            return db.USUARIO;
        }

        // GET: api/Users/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult GetUSUARIO(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);
            if (uSUARIO == null)
            {
                return NotFound();
            }

            return Ok(uSUARIO);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult PutUSUARIO(int id, USUARIO uSUARIO)
        {
            if (id != uSUARIO.COD_USUARIO)
            {
                return BadRequest();
            }

            uSUARIO.ATIVO = 1;
            uSUARIO.SENHA = Encrypt.CalculateMD5Hash(uSUARIO.SENHA);

            db.Entry(uSUARIO).State = EntityState.Modified;
            //db.Entry(uSUARIO).Property(x => x.SENHA).IsModified = false;

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
                    throw new DbUpdateConcurrencyException();
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult PostUSUARIO(USUARIO uSUARIO)
        {
            uSUARIO.INSCRICAO = new HashSet<INSCRICAO>();
            uSUARIO.USUARIO_GERENCIA_EVENTO = new HashSet<USUARIO_GERENCIA_EVENTO>();
            uSUARIO.PERFIL = null;
            uSUARIO.SENHA = Encrypt.CalculateMD5Hash(uSUARIO.SENHA);
            uSUARIO.ATIVO = 1;

            db.USUARIO.Add(uSUARIO);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = uSUARIO.COD_USUARIO }, uSUARIO);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(USUARIO))]
        public IHttpActionResult DeleteUSUARIO(int id)
        {
            USUARIO uSUARIO = db.USUARIO.Find(id);

            if (uSUARIO == null)
            {
                return NotFound();
            }
            else if (uSUARIO.ATIVO == 0)
            {
                uSUARIO.ATIVO = 1;
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                uSUARIO.ATIVO = 0;
                db.Entry(uSUARIO).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Ok(uSUARIO);
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
            return db.USUARIO.Count(e => e.COD_USUARIO == id) > 0;
        }
    }
}