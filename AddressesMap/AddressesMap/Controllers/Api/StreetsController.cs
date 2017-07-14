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
using AddressesMap.Models;
using AddressesMap.Models.DBModels;

namespace AddressesMap.Controllers.Api
{
    public class StreetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Streets
        public IQueryable<Street> GetStreets()
        {
            return db.Streets;
        }

        // GET: api/Streets/5
        [ResponseType(typeof(Street))]
        public async Task<IHttpActionResult> GetStreet(int id)
        {
            Street street = await db.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            return Ok(street);
        }

        // PUT: api/Streets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStreet(int id, Street street)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != street.StreetId)
            {
                return BadRequest();
            }

            db.Entry(street).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreetExists(id))
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

        // POST: api/Streets
        [ResponseType(typeof(Street))]
        public async Task<IHttpActionResult> PostStreet(Street street)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Streets.Add(street);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = street.StreetId }, street);
        }

        // DELETE: api/Streets/5
        [ResponseType(typeof(Street))]
        public async Task<IHttpActionResult> DeleteStreet(int id)
        {
            Street street = await db.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            db.Streets.Remove(street);
            await db.SaveChangesAsync();

            return Ok(street);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StreetExists(int id)
        {
            return db.Streets.Count(e => e.StreetId == id) > 0;
        }
    }
}