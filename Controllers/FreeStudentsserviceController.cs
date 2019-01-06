using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using alsyedAcademy.Models;

namespace alsyedAcademy.Controllers
{
    public class FreeStudentsserviceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FreeStudentsservice
        public IQueryable<FreeStudents> GetFreeStudents()
        {
            return db.FreeStudents;
        }

        // GET: api/FreeStudentsservice/5
        [ResponseType(typeof(FreeStudents))]
        public async Task<IHttpActionResult> GetFreeStudents(int id)
        {
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            if (freeStudents == null)
            {
                return NotFound();
            }

            return Ok(freeStudents);
        }

        // PUT: api/FreeStudentsservice/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFreeStudents(int id, FreeStudents freeStudents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != freeStudents.id)
            {
                return BadRequest();
            }

            db.Entry(freeStudents).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FreeStudentsExists(id))
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

        // POST: api/FreeStudentsservice
        [ResponseType(typeof(FreeStudents))]
        public async Task<IHttpActionResult> PostFreeStudents(FreeStudents freeStudents)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FreeStudents.Add(freeStudents);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = freeStudents.id }, freeStudents);
        }

        // DELETE: api/FreeStudentsservice/5
        [ResponseType(typeof(FreeStudents))]
        public async Task<IHttpActionResult> DeleteFreeStudents(int id)
        {
            FreeStudents freeStudents = await db.FreeStudents.FindAsync(id);
            if (freeStudents == null)
            {
                return NotFound();
            }

            db.FreeStudents.Remove(freeStudents);
            await db.SaveChangesAsync();

            return Ok(freeStudents);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FreeStudentsExists(int id)
        {
            return db.FreeStudents.Count(e => e.id == id) > 0;
        }
    }
}