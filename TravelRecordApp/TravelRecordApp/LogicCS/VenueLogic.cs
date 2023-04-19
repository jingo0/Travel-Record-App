using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;

namespace TravelRecordApp.LogicCS
{
    internal class VenueLogic
    {
        public async static Task<List<VenueRoot>> GetVenues(double latitude,  double longitude)
        {
            List<VenueRoot> venues = new List<VenueRoot>();

            var url = VenueRoot.GenerateUrl(latitude, longitude);

            using(HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");

                request.Headers.TryAddWithoutValidation("Authorization", Constants.API_KEY);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.results as List<VenueRoot>;

            }

            //Debug.WriteLine(venues);

            return venues;
        }
    }
}
