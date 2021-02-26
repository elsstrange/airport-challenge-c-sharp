using System;

public class Plane
{
	private bool InFlight
	{ get; set; }

	public Plane()
	{
		InFlight = false;
	}

	public virtual void TakeOff()
    {
		if(InFlight)
        {
			throw new InvalidOperationException("Cannot Take Off: Already in flight");
        }
		InFlight = true;
    }

	public virtual void Land()
    {
		if(!InFlight)
        {
			throw new InvalidOperationException("Cannot Land: Already landed");
		}
		InFlight = false;
    }
}
