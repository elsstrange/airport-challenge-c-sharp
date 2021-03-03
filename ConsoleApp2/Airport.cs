using System;
using System.Collections.Generic;

namespace AirportChallenge
{
    public class Airport
    {
        public List<Plane> Planes
        {
            get; private set;
        }
        private int Capacity
        { get; set; }

        private Weather Weather
        { get; set; }

        public Airport(Weather weather, int capacity = 10)
        {
            Planes = new List<Plane>();
            Capacity = capacity;
            Weather = weather;
        }

        public void Land(Plane plane)
        {
            PreLandingChecks();
            try
            {
                plane.Land();
                Planes.Add(plane);
            }
            catch
            {
                throw new InvalidOperationException("Cannot land: Plane rejected instruction to land");
            }
        }

        public void TakeOff(Plane plane)
        {
            PreTakeOffChecks(plane);
            plane.TakeOff();
            Planes.Remove(plane);
        }

        private void PreLandingChecks()
        {
            CheckCapacity();
            CheckWeather();
        }

        private void PreTakeOffChecks(Plane plane)
        {
            CheckPlaneIsPresent(plane);
            CheckWeather();
        }

        private void CheckPlaneIsPresent(Plane plane)
        {
            if (!Planes.Contains(plane))
            {
                throw new InvalidOperationException("Cannot take off: Specified plane is not present");
            }
        }

        private void CheckCapacity()
        {
            if (Planes.Count >= Capacity)
            {
                throw new InvalidOperationException("Cannot land: No capacity for additional planes");
            }
        }

        private void CheckWeather()
        {
            if (!Weather.IsFine())
            {
                throw new InvalidOperationException("Invalid Operation: Weather is too poor");
            }
        }
    }
}
