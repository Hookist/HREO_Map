using AddressesMap.Models.DBModels;
using Google.Maps.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;

namespace AddressesMap.Controllers
{
    public class HRUAController : ApiController
    {
        private AddressesMapModel adrModel = new AddressesMapModel();
        // GET: HRUA
        //[HttpGet]
        //public HttpResponseMessage Get(HttpRequestMessage request)
        //{
        //    var addresses1 = new List<int> { 1, 2, 3, 4 };
        //        return request.CreateResponse(HttpStatusCode.OK, addresses1);
        //}

        private GeocodeResponse TakeCoordinates(string address)
        {
            var request = new GeocodingRequest();
            request.Address = address;
            request.Sensor = false;
            return new GeocodingService().GetResponse(request);
        }

        public HttpResponseMessage FillCoordinates(HttpRequestMessage request)
        {
            var addresses = (from adr in adrModel.Addresses
                             join str in adrModel.Streets
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
                var House = adrModel.Addresses.Where(a => a.AddressId == address.addressId).First();
                House.Latitude = (decimal)location.Select(p => p.Latitude).FirstOrDefault();
                House.Longitude = Convert.ToDecimal(location.Select(p => p.Longitude).FirstOrDefault());
                adrModel.SaveChanges();
            }
            //adrModel.SaveChanges();
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Address>))]
        public HttpResponseMessage GetAddresses(HttpRequestMessage request)
        {
            var addresses = adrModel.Addresses.Where(a => a.AddressId < 884).ToList();
            return request.CreateResponse(HttpStatusCode.OK, addresses);
        }
    }
}
