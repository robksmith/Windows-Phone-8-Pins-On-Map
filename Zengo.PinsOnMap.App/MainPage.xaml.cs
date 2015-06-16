#region Usings

using Zengo.PinsOnMap.App.Controls;
using Zengo.PinsOnMap.App.Helper;
using Zengo.PinsOnMap.App.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Device.Location;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

#endregion


namespace Zengo.PinsOnMap.App
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Fields

        // Our temporary app state
        AppState appState;

        // The map object
        Map mainMap;
    
        // The map overlay which holds the points
        MapLayer mainLayer;

        #endregion


        #region Constructors

        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            BuildLocalizedApplicationBar();

            // Create a map
            mainMap = new Map();

            // Get the app state
            appState = App.AppState;

            // Add the map to the controls collection
            ContentPanel.Children.Add(mainMap);

            // Initialise the map and restore state
            InitialiseTheMap();
        }

        #endregion


        #region Event Handlers

        /// <summary>
        /// Our tap event on the map
        /// </summary>
        void pinsOnMap_Tap(object sender, GestureEventArgs e)
        {
            if (sender is Map)
            {
                // Make sure its our map which has sent this tap
                Map m = sender as Map;

                // Get the clicked 2D point
                Point p = e.GetPosition(mainMap);

                // Convert it to a geocoordinate
                GeoCoordinate geo = new GeoCoordinate();
                geo = mainMap.ConvertViewportPointToGeoCoordinate(p);

                // Get the time
                DateTime now = DateTime.Now;

                // Creat a new point
                AppState.PointOnMap clickedPoint = new AppState.PointOnMap() { GeoCoordinate = geo, TimeStamp = now };

                // add the point to our state
                appState.AddPoint(clickedPoint);

                // And add the point to our map
                AddPointToLayer(clickedPoint, mainLayer);
            }
        }


        /// <summary>
        /// Our app bar clear the map event
        /// </summary>
        void clearMapButton_Click(object sender, EventArgs e)
        {
            // Remove from state
            appState.Clear();

            // Clear the map
            mainLayer.Clear();
        }

        #endregion


        #region Map Helpers

        /// <summary>
        /// Initialise the map and recreate the points from the saved state
        /// </summary>
        private void InitialiseTheMap()
        {
            // Add a tap event to the map to detect the clicks
            mainMap.Tap += pinsOnMap_Tap;

            // Berlin centre
            GeoCoordinate berlinGeoCoordinate = new GeoCoordinate(+52.5, +13.4);

            // Make the centre of the map Berlin as per the instructions.
            mainMap.Center = berlinGeoCoordinate;
            mainMap.ZoomLevel = 10;

            // Create a MapLayer to contain pins.
            mainLayer = new MapLayer();

            // Add the MapLayer to the Map.
            mainMap.Layers.Add(mainLayer);

            // For every point in our saved state, recreate the map by adding the point
            foreach (AppState.PointOnMap p in appState.Points)
            {
                AddPointToLayer(p, mainLayer);
            }
        }

        /// <summary>
        /// Add a point to a specified map layer
        /// </summary>
        private void AddPointToLayer(AppState.PointOnMap p, MapLayer layer)
        {
            // Create a pin control - look in the Controls directory to find our pin definition
            PinOnMap pinOnMap = new PinOnMap();
            pinOnMap.Text = p.TimeStamp.ToLongTimeString();

            // Create a MapOverlay to contain the pin.
            MapOverlay pinOverlay = new MapOverlay();
            pinOverlay.Content = pinOnMap;
            // offset the pin - because we want it drawn from where the point on the pin is annd not the centre
            pinOverlay.PositionOrigin = new Point(0.5, 1);   
            pinOverlay.GeoCoordinate = p.GeoCoordinate;

            // Add it to the specified layer
            layer.Add(pinOverlay);
        }

        #endregion


        #region App Bar

        /// <summary>
        /// Building an simple ApplicationBar
        /// </summary>
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton clearMapButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/cancel.png", UriKind.Relative));
            clearMapButton.Click += clearMapButton_Click;
            clearMapButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(clearMapButton);
        }


        #endregion

    }
}