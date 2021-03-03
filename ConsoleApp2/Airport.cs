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
            var forecast = Weather.IsFine();
            if (IsFull())
            {
                throw new InvalidOperationException("Cannot land: No capacity for additional planes");
            }
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
            if (PlaneIsNotPresent(plane))
            {
                throw new InvalidOperationException("Cannot take off: Specified plane is not present");
            }
            plane.TakeOff();
            Planes.Remove(plane);
        }

        private bool IsFull()
        {
            return Planes.Count >= Capacity;
        }

        private bool PlaneIsNotPresent(Plane plane)
        {
            return !Planes.Contains(plane);
        }
    }
}
