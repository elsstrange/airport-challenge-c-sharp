using System;
using NUnit.Framework;
using FakeItEasy;

namespace AirportChallenge.Tests
{
    [TestFixture]
    public class WeatherTest
    {
        /* 
        * In the domain of this little exercise, a call to Next on an instance of System.Random
        * stands in for calling a weather API or similar. If the return value is 10, the weather
        * is considered to not be sunny. 1-9 are considered sunny.
        */
        [Test]
        public void IsFine_should_return_true_when_forecast_is_sunny()
        {
            var sunnyRandom = A.Fake<Random>();
            A.CallTo(() => sunnyRandom.Next()).Returns(1);
            Weather testWeather = new Weather(sunnyRandom);
            Assert.That(testWeather.IsFine(), Is.EqualTo(true));
        }
    }
}
