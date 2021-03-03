using System;
using NUnit.Framework;
using FakeItEasy;

namespace AirportChallenge.Tests
{
    [TestFixture]
    public class AirportTest
    {
        Airport sunnyAirport, airportWithPlane;
        Plane testLandedPlane, testInFlightPlane, testPlaneForTakeOff;
        Weather sunnyWeather, rainyWeather;
        readonly string fullAirportError = "Cannot land: No capacity for additional planes";
        readonly string weatherError = "Invalid Operation: Weather is too poor";
        readonly string planeLandError = "Cannot land: Plane rejected instruction to land";
        readonly string planeTakeOffError = "Cannot take off: Plane rejected instruction to take off";
        readonly string planeNotPresentError = "Cannot take off: Specified plane is not present";

        [SetUp]
        public void Setup()
        {
            sunnyWeather = A.Fake<Weather>();
            A.CallTo(() => sunnyWeather.IsFine()).Returns(true);

            rainyWeather = A.Fake<Weather>();
            A.CallTo(() => rainyWeather.IsFine()).Returns(false);        

            testLandedPlane = A.Fake<Plane>();
            A.CallTo(() => testLandedPlane.Land()).Throws<InvalidOperationException>();
            
            testInFlightPlane = A.Fake<Plane>();
            A.CallTo(() => testInFlightPlane.TakeOff()).Throws<InvalidOperationException>();
            
            testPlaneForTakeOff = A.Fake<Plane>();

            sunnyAirport = new Airport(sunnyWeather);
            
            airportWithPlane = new Airport(sunnyWeather);
            airportWithPlane.Land(testPlaneForTakeOff);
        }

        [Test]
        public void Land_should_add_plane_to_Planes_list()
        {
            sunnyAirport.Land(testInFlightPlane);
            Assert.That(sunnyAirport.Planes, Has.Member(testInFlightPlane));
        }

        [Test]
        public void Land_should_not_land_plane_if_airport_is_full_at_default_capacity_10()
        {
            for(var i = 0; i < 10; i++)
            {
                sunnyAirport.Land(A.Fake<Plane>());
            }
            Assert.That(
                () => { sunnyAirport.Land(testInFlightPlane); },
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
            sunnyAirport.Land(testInFlightPlane);
            A.CallTo(() => testInFlightPlane.Land()).MustHaveHappened();
        }

        [Test]
        public void Land_should_not_land_a_plane_if_plane_rejects_landing_instruction()
        {
            Assert.That(
                () => { sunnyAirport.Land(testLandedPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeLandError));
        }

        [Test]
        public void Land_should_check_weather()
        {
            sunnyAirport.Land(testInFlightPlane);
            A.CallTo(() => sunnyWeather.IsFine()).MustHaveHappened();
        }

        [Test]
        public void Land_should_not_land_a_plane_if_weather_is_poor()
        {
            Airport rainyAirport = new Airport(rainyWeather);
            Assert.That(
                () => { rainyAirport.Land(testInFlightPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(weatherError));
        }

        [Test]
        public void TakeOff_should_remove_plane_from_Planes_list()
        {
            airportWithPlane.TakeOff(testPlaneForTakeOff);
            Assert.That(airportWithPlane.Planes, Has.No.Member(testPlaneForTakeOff));
        }

        [Test]
        public void TakeOff_should_throw_error_if_specified_plane_is_not_present()
        {           
            Assert.That(
                () => { sunnyAirport.TakeOff(testPlaneForTakeOff); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeNotPresentError));
        }

        [Test]
        public void TakeOff_should_only_remove_specified_plane_from_Planes_list()
        {
            var testPlane1 = A.Fake<Plane>();
            var testPlane2 = A.Fake<Plane>();
            sunnyAirport.Land(testPlane1);
            sunnyAirport.Land(testPlane2);
            sunnyAirport.TakeOff(testPlane1);
            Assert.That(sunnyAirport.Planes, Has.Member(testPlane2));
        }

        [Test]
        public void TakeOff_should_call_TakeOff_on_plane()
        {
            airportWithPlane.TakeOff(testPlaneForTakeOff);
            A.CallTo(() => testPlaneForTakeOff.TakeOff()).MustHaveHappened();
        }

        [Test]
        public void TakeOff_should_not_take_off_a_plane_if_plane_rejects_take_off_instruction()
        {
            sunnyAirport.Land(testInFlightPlane);
            // Exploiting the fact that the mock plane does not change its in flight status on landing like a real plane would.
            Assert.That(
                () => { sunnyAirport.TakeOff(testInFlightPlane); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(planeTakeOffError));
        }

        [Test]
        public void TakeOff_should_check_weather()
        {
            airportWithPlane.TakeOff(testPlaneForTakeOff);
            // Test has to specify two calls because weather is checked as part of set up.
            A.CallTo(() => sunnyWeather.IsFine()).MustHaveHappenedTwiceExactly();
        }

        [Test]
        public void TakeOff_should_not_take_off_a_plane_if_weather_is_poor()
        {
            // Flip the weather
            A.CallTo(() => sunnyWeather.IsFine()).Returns(false);

            Assert.That(
                () => { airportWithPlane.TakeOff(testPlaneForTakeOff); },
                Throws.InvalidOperationException
                .With.Property("Message").EqualTo(weatherError));
        }
    }
}