using NUnit.Framework;

namespace AirportChallenge.Tests
{
	[TestFixture]
	public class PlaneTest
	{
		Plane testPlane;

		[SetUp]
		public void SetUp()
        {
			testPlane = new Plane();
        }

		[Test]
		public void TakeOff_should_execute_if_plane_is_landed()
        {
			Assert.That(
				() => { testPlane.TakeOff(); },
				Throws.Nothing);
        }
	}

}
