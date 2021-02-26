using NUnit.Framework;
using FakeItEasy;

namespace AirportChallenge.Tests
{
    [TestFixture]
    public class AirportTest
    {
        Airport testAirport;
        Plane testPlane;
        string fullAirportError;

        [SetUp]
        public void Setup()
        {
            testAirport = new Airport();
            testPlane = A.Fake<Plane>();
            fullAirportError = "Cannot land: No capacity for additional planes";
        }

        [Test]
        public void Land_should_add_plane_to_Planes_list()
        {
            testAirport.Land(testPlane);
            Assert.That(testAirport.Planes, Has.Member(testPlane));
        }

        [Test]
        public void Land_should_not_land_plane_if_airport_is_full_at_default_capacity_10()
        {
            for(var i = 0; i < 10; i++)
            {
                testAirport.Land(A.Fake<Plane>());
            }
            Assert.That(
                () => { testAirport.Land(testPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(fullAirportError));
        }

        [Test]
        public void Land_should_not_land_plane_if_airport_is_full_at_custom_capacity()
        {
            Airport testAirport = new Airport(1);
            testAirport.Land(A.Fake<Plane>());
            Assert.That(
                () => { testAirport.Land(testPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(fullAirportError));
        }

        [Test]
        public void TakeOff_should_remove_plane_from_Planes_list()
        {
            testAirport.Land(testPlane);
            testAirport.TakeOff(testPlane);
            Assert.That(testAirport.Planes, Has.No.Member(testPlane));
        }

        [Test]
        public void TakeOff_should_throw_error_if_specified_plane_is_not_present()
        {
            string planeNotPresentError = "Cannot take off: Specified plane is not present";
            
            Assert.That(
                () => { testAirport.TakeOff(testPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeNotPresentError));
        }

        [Test]
        public void TakeOff_should_only_remove_specified_plane_from_Planes_list()
        {
            
            var testPlane1 = A.Fake<Plane>();
            var testPlane2 = A.Fake<Plane>();
            testAirport.Land(testPlane1);
            testAirport.Land(testPlane2);
            testAirport.TakeOff(testPlane1);
            Assert.That(testAirport.Planes, Has.Member(testPlane2));
        }
    }
}