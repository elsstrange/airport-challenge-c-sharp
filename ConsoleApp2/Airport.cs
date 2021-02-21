using System;
using System.Collections.Generic;

namespace AirportChallenge
{
    public class Airport
    {
        public List<object> Planes
        {
            get; private set;
        }
        
        public Airport()
        {
            Planes = new List<object>();
        }

        public void Land(object plane)
        {
            Planes.Add(plane);
        }
    }
}
