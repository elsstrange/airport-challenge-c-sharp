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
        
        public Airport()
        {
            Planes = new List<Plane>();
        }

        public void Land(Plane plane)
        {
            Planes.Add(plane);
        }
    }
}
