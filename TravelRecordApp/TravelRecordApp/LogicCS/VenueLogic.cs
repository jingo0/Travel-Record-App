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
        public async static Task<List<Venue>> GetVenues(double latitude,  double longitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = Venue.GenerateUrl(latitude, longitude);

            using(HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");

                request.Headers.TryAddWithoutValidation("Authorization", Constants.API_KEY);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(request);
                Console.WriteLine("json: " + json);

            }
            return venues;
        }
    }
}
