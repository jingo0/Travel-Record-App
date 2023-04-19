using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        IGeolocator locator = CrossGeolocator.Current;
        public MapPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			GetLocation();

            GetPosts();
        }

        private void GetPosts()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DataBaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();

                DisplayOnMap(posts);
            }
        }

        private void DisplayOnMap(List<Post> posts)
        {
            foreach (var post in posts)
            {
                try
                {
                    var pinCoords = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);

                    var pin = new Pin()
                    {
                        Position = pinCoords,
                        Label = post.VenueName,
                        Address = post.Address,
                        Type = PinType.SavedPin
                    };

                    locationMap.Pins.Add(pin);
                }
                catch (Exception ex)
                { 

                }

            }
        }


        async protected override void OnDisappearing()
        {
            base.OnDisappearing();

            await locator.StopListeningAsync();
        }

        async private void GetLocation()
        {
            var status = await checkAndRequestLocationPermission();

            if(status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                if (!CrossGeolocator.Current.IsListening)
                {

                    locator.PositionChanged += Locator_PositionChanged;

                    await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

                    locationMap.IsShowingUser = true;

                    centerMap(location.Latitude, location.Longitude);
                }
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            centerMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void centerMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position coordinates = new Xamarin.Forms.Maps.Position(latitude, longitude);
            MapSpan span = new MapSpan(coordinates, 1, 1);
            locationMap.MoveToRegion(span);
        }

        async private Task<PermissionStatus> checkAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if(status == PermissionStatus.Granted)
            {
                return status;
            }

            if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.Android) 
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                return status;
            }

            return status;
            
        }
    }
}