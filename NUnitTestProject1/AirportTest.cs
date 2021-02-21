using NUnit.Framework;

namespace AirportChallenge.Tests
{
    [TestFixture]
    public class AirportTest
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void Land()
        {
            object testPlane = new object();
            Airport testAirport = new Airport();
            testAirport.Land(testPlane);
            Assert.IsTrue(testAirport.Planes.Contains(testPlane));
        }
    }
}