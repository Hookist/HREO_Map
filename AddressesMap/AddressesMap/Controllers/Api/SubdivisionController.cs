using AddressesMap.Models.DBModels;
using Google.Maps.Geocoding;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AddressesMap.Controllers.Api
{
    public class SubdivisionController : ApiController
    {
        private AddressesMapModel db = new AddressesMapModel();

        public IQueryable<Subdivision> GetSubvisions()
        {
            return db.Subdivisions;
        }

        // GET: api/Subdivision/5
        [ResponseType(typeof(Subdivision))]
        public async Task<IHttpActionResult> GetSubdivision(int id)
        {
            Subdivision sub = await db.Subdivisions.FindAsync(id);
            if (sub == null)
            {
                return NotFound();
            }
            return Ok(sub);
        }

        // PUT: api/Subdivision/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubdivision(int id, Subdivision subdivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subdivision.SubdivisionId)
            {
                return BadRequest();
            }

            db.Entry(subdivision).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubdivisionExists(id))
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

        // POST: api/Subdivision
        [ResponseType(typeof(Subdivision))]
        public async Task<IHttpActionResult> PostSubdivision(Subdivision subdivision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subdivisions.Add(subdivision);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = subdivision.SubdivisionId }, subdivision);
        }

        // DELETE: api/Subdivision/5
        [ResponseType(typeof(Subdivision))]
        public async Task<IHttpActionResult> DeleteSubdivision(int id)
        {
            Subdivision subdivision = await db.Subdivisions.FindAsync(id);
            if (subdivision == null)
            {
                return NotFound();
            }

            db.Subdivisions.Remove(subdivision);
            await db.SaveChangesAsync();

            return Ok(subdivision);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubdivisionExists(int id)
        {
            return db.Subdivisions.Count(e => e.SubdivisionId == id) > 0;
        }

    }
}
