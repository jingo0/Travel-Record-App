using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    internal class Venue
    {
        public static string GenerateUrl(double latitude, double longitude)
        {
            string url = string.Format(Helpers.Constants.VENUE_SEARCH, latitude, longitude, Helpers.Constants.CLIENT_ID, Helpers.Constants.CLIENT_SECRET, DateTime.Now.ToString("yyyyMMdd"));

            return url;
        }
    }
}
