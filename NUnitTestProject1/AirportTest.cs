using NUnit.Framework;
using FakeItEasy;

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
        public void Land_should_add_plane_to_Planes_list()
        {
            var testPlane = A.Fake<Plane>();
            Airport testAirport = new Airport();
            testAirport.Land(testPlane);
            Assert.That(testAirport.Planes, Has.Member(testPlane));
        }
    }
}