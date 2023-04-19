using Plugin.Geolocator;
using SQLite;
using System;
using System.Linq;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);

            venueListView.ItemsSource = venues;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = venueListView.SelectedItem as Result;
                var firstCategory = selectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.distance,
                    Latitude = selectedVenue.geocodes.main.latitude,
                    Longitude = selectedVenue.geocodes.main.longitude,
                    VenueName = selectedVenue.name,
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DataBaseLocation))
                {
                    conn.CreateTable<Post>();
                    int rows = conn.Insert(post);
                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience sucessfully inserted", "Ok");
                        experienceEntry.Text = string.Empty;
                    }
                    else
                    {
                        DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
                    }
                }
            }
            catch(NullReferenceException nre) 
            {
                throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }       
			
        }

        void venueListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedVenue = venueListView.SelectedItem as Result;

            experienceEntry.Text = selectedVenue.name;
        }
    }
}