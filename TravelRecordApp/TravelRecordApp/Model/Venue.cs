using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    internal class Venue
    {
        public static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Helpers.Constants.VENUE_SEARCH, latitude, longitude);
        }
    }
}
