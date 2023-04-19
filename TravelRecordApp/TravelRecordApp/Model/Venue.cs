using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
   
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Chain
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Roof
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Geocodes
    {
        public Main main { get; set; }
        public Roof roof { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string address_extended { get; set; }
        public string census_block { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string dma { get; set; }
        public string formatted_address { get; set; }
        public string locality { get; set; }
        public IList<string> neighborhood { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
    }

    public class Parent
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public Parent parent { get; set; }
    }

    public class Result
    {
        public string fsq_id { get; set; }
        public IList<Category> categories { get; set; }
        public IList<Chain> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public string link { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }
    }

    public class Center
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Circle
    {
        public Center center { get; set; }
        public int radius { get; set; }
    }

    public class GeoBounds
    {
        public Circle circle { get; set; }
    }

    public class Context
    {
        public GeoBounds geo_bounds { get; set; }
    }

    /*public class Venue
    {
        public IList<Result> results { get; set; }
    }*/

    public class VenueRoot
    {
        public IList<Result> results { get; set; }
        public Context context { get; set; }
        public static string GenerateUrl(double latitude, double longitude)
        {
            return string.Format(Helpers.Constants.VENUE_SEARCH, latitude, longitude);
        }
    }
}
