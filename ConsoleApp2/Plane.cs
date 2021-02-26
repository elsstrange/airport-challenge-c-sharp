using System;

public class Plane
{
	private bool InFlight
	{ get; set; }

	public Plane()
	{
		InFlight = false;
	}

	public void TakeOff()
    {
		if(InFlight)
        {
			throw new InvalidOperationException("Cannot Take Off: Already in flight");
        }
		InFlight = true;
    }

	public void Land()
    {
		if(!InFlight)
        {
			throw new InvalidOperationException("Cannot Land: Already landed");
		}
		InFlight = false;
    }
}
