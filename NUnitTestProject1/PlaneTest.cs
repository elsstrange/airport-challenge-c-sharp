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

		[Test]
		public void TakeOff_should_throw_error_if_plane_is_in_flight()
        {
			string takeOffError = "Cannot Take Off: Already in flight";
			testPlane.TakeOff();
			Assert.That(
				() => { testPlane.TakeOff(); },
				Throws.InvalidOperationException
                .With.Property("Message").EqualTo(takeOffError));
        }

		[Test]
		public void Land_should_execute_if_plane_is_in_flight()
        {
			testPlane.TakeOff();
			Assert.That(
				() => { testPlane.Land(); },
				Throws.Nothing);
        }

		[Test]
		public void Land_should_throw_error_if_plane_is_landed()
		{
			string landingError = "Cannot Land: Already landed";
			Assert.That(
				() => { testPlane.Land(); },
				Throws.InvalidOperationException
				.With.Property("Message").EqualTo(landingError));
		}

		[Test]
		public void Land_should_allow_subsequent_take_off()
        {
			testPlane.TakeOff();
			testPlane.Land();
			Assert.That(
				() => { testPlane.TakeOff(); },
				Throws.Nothing);
        }
	}

}
