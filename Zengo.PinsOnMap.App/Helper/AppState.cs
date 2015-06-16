#region Usings

using System;
using System.Collections.Generic;
using System.Device.Location;

#endregion

namespace Zengo.PinsOnMap.App.Helper
{
    /// <summary>
    /// For this demo, AppState holds the state of the pins. This is basically a list of geo-coords with a timestamp
    /// </summary>
    public class AppState
    {
        public class PointOnMap
        {
            #region Properties

            public GeoCoordinate GeoCoordinate { get; set; }
            public DateTime TimeStamp { get; set; }

            #endregion
        }

        #region Properties

        /// <summary>
        /// Our list of points
        /// </summary>
        public List<PointOnMap> Points { get; private set; }

        #endregion


        #region Constructors

        public AppState()
        {
            Points = new List<PointOnMap>();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Add a point to our list
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(PointOnMap point)
        {
            Points.Add(point);
        }

        /// <summary>
        /// Clear all points
        /// </summary>
        internal void Clear()
        {
            Points.Clear();
        }

        #endregion

    }
}
