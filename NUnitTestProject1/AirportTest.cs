using System;
using NUnit.Framework;
using FakeItEasy;

namespace AirportChallenge.Tests
{
    [TestFixture]
    public class AirportTest
    {
        Airport testSunnyAirport;
        Plane testLandedPlane, testInFlightPlane, testPlaneForTakeOff;
        Weather sunnyWeather;
        string fullAirportError;

        [SetUp]
        public void Setup()
        {
            sunnyWeather = A.Fake<Weather>();
            A.CallTo(() => sunnyWeather.IsFine()).Returns(true);

            testSunnyAirport = new Airport(sunnyWeather);          

            testLandedPlane = A.Fake<Plane>();
            A.CallTo(() => testLandedPlane.Land()).Throws<InvalidOperationException>();
            
            testInFlightPlane = A.Fake<Plane>();
            A.CallTo(() => testInFlightPlane.TakeOff()).Throws<InvalidOperationException>();
            
            testPlaneForTakeOff = A.Fake<Plane>();
            fullAirportError = "Cannot land: No capacity for additional planes";
        }

        [Test]
        public void Land_should_add_plane_to_Planes_list()
        {
            testSunnyAirport.Land(testInFlightPlane);
            Assert.That(testSunnyAirport.Planes, Has.Member(testInFlightPlane));
        }

        [Test]
        public void Land_should_not_land_plane_if_airport_is_full_at_default_capacity_10()
        {
            for(var i = 0; i < 10; i++)
            {
                testSunnyAirport.Land(A.Fake<Plane>());
            }
            Assert.That(
                () => { testSunnyAirport.Land(testInFlightPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(fullAirportError));
        }

        [Test]
        public void Land_should_not_land_plane_if_airport_is_full_at_custom_capacity()
        {
            Airport smallAirport = new Airport(sunnyWeather, 1);
            smallAirport.Land(A.Fake<Plane>());
            Assert.That(
                () => { smallAirport.Land(testInFlightPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(fullAirportError));
        }

        [Test]
        public void Land_should_call_Land_on_plane()
        {
            testSunnyAirport.Land(testInFlightPlane);
            A.CallTo(() => testInFlightPlane.Land()).MustHaveHappened();
        }

        [Test]
        public void Land_should_not_land_a_plane_if_plane_rejects_landing_instruction()
        {
            string planeLandError = "Cannot land: Plane rejected instruction to land";
            Assert.That(
                () => { testSunnyAirport.Land(testLandedPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeLandError));
        }

        [Test]
        public void Land_should_check_weather()
        {
            testSunnyAirport.Land(testInFlightPlane);
            A.CallTo(() => sunnyWeather.IsFine()).MustHaveHappened();
        }

        [Test]
        public void TakeOff_should_remove_plane_from_Planes_list()
        {
            testSunnyAirport.Land(testPlaneForTakeOff);
            testSunnyAirport.TakeOff(testPlaneForTakeOff);
            Assert.That(testSunnyAirport.Planes, Has.No.Member(testPlaneForTakeOff));
        }

        [Test]
        public void TakeOff_should_throw_error_if_specified_plane_is_not_present()
        {
            string planeNotPresentError = "Cannot take off: Specified plane is not present";
            
            Assert.That(
                () => { testSunnyAirport.TakeOff(testPlaneForTakeOff); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeNotPresentError));
        }

        [Test]
        public void TakeOff_should_only_remove_specified_plane_from_Planes_list()
        {
            
            var testPlane1 = A.Fake<Plane>();
            var testPlane2 = A.Fake<Plane>();
            testSunnyAirport.Land(testPlane1);
            testSunnyAirport.Land(testPlane2);
            testSunnyAirport.TakeOff(testPlane1);
            Assert.That(testSunnyAirport.Planes, Has.Member(testPlane2));
        }

        [Test]
        public void TakeOff_should_call_TakeOff_on_plane()
        {
            testSunnyAirport.Land(testPlaneForTakeOff);
            testSunnyAirport.TakeOff(testPlaneForTakeOff);
            A.CallTo(() => testPlaneForTakeOff.TakeOff()).MustHaveHappened();
        }

        [Test]
        public void TakeOff_should_not_take_off_a_plane_if_plane_rejects_take_off_instruction()
        {

        }
    }
}