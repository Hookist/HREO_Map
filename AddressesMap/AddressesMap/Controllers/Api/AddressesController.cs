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
using AddressesMap.Models.DBModels;
using Google.Maps.Geocoding;
using System.Threading;

namespace AddressesMap.Controllers.Api
{
    public class AddressesController : ApiController
    {
        private AddressesMapModel db = new AddressesMapModel();

        // GET: api/Addresses
        [HttpGet]
        public IQueryable<Address> Get()
        {
            return db.Addresses;
        }

        // GET: api/Addresses/5
        [HttpGet]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> GetAddress(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAddress(int id, Address address)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != address.AddressId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(address).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AddressExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [HttpPost]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Addresses.Add(address);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            Address address = await db.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            db.Addresses.Remove(address);
            await db.SaveChangesAsync();

            return Ok(address);
        }

        //GET: api/Addresses/AddressesBySubdivision/5
        [HttpGet]
        public IQueryable<Address> AddressesBySubdivision (int id)
        {
            if(id == 0)
            {
                return null;
            }
            return db.Addresses.Where(a => a.SubdivisionId == id && a.AddressId < 500);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return db.Addresses.Count(e => e.AddressId == id) > 0;
        }

        private GeocodeResponse TakeCoordinates(string address)
        {
            var request = new GeocodingRequest();
            request.Address = address;
            request.Sensor = false;
            return new GeocodingService().GetResponse(request);
        }

        public HttpResponseMessage FillCoordinates(HttpRequestMessage request)
        {
            var addresses = (from adr in db.Addresses
                             join str in db.Streets
                             on adr.StreetId equals str.StreetId
                             where adr.AddressId > 883
                             select new
                             {
                                 addressId = adr.AddressId,
                                 houseAddress = "Киев " + str.StreetName + " " + adr.House
                             }).ToList();
            foreach (var address in addresses)
            {
                var responce = TakeCoordinates(address.houseAddress);
                Thread.Sleep(1000);
                var location = responce.Results.Select(p => p.Geometry.Location);
                var House = db.Addresses.Where(a => a.AddressId == address.addressId).First();
                House.Latitude = (decimal)location.Select(p => p.Latitude).FirstOrDefault();
                House.Longitude = Convert.ToDecimal(location.Select(p => p.Longitude).FirstOrDefault());
                db.SaveChanges();
            }
            //adrModel.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK);
        }
    }
}