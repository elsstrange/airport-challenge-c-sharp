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
            if (IsFull())
            {
                throw new InvalidOperationException("Cannot land: No capacity for additional planes");
            }
            Planes.Add(plane);
        }

        private bool IsFull()
        {
            return Planes.Count >= 10;
        }
    }
}
