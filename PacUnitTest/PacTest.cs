using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pac;
using Pac.Model;

namespace PacUnitTest
{
    [TestClass]
    public class PacTest
    {

        [TestMethod]
        public void PacActOnPerson_NoPerson()
        {
            var deviceMock = new Mock<IDevice>();

            deviceMock.Setup(foo => foo.On()).Verifiable();
            deviceMock.Setup(foo => foo.Off()).Verifiable();
            deviceMock.Setup(foo => foo.Restore()).Verifiable();

            var deivce = deviceMock.Object;


            var zoneMock = new Mock<Zone>();
            zoneMock.Setup(foo => foo.InZone(It.IsAny<Person>())).Returns(false).Verifiable();

            var zone = zoneMock.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce },
                Identifier = "mock1",
                Zone = zone
            });

            pac.ActOnPeoplePresent(new List<Person>());

            deviceMock.Verify(t => t.Restore(), Times.Never);
            deviceMock.Verify(t => t.Off(), Times.Once);
            deviceMock.Verify(t => t.On(), Times.Never);
            zoneMock.Verify(t => t.InZone(It.IsAny<Person>()), Times.Never);
        }

        [TestMethod]
        public void PacActOnPerson_OnePerson_ZoneTrue()
        {
            var deviceMock = new Mock<IDevice>();

            deviceMock.Setup(foo => foo.On()).Verifiable();
            deviceMock.Setup(foo => foo.Off()).Verifiable();
            deviceMock.Setup(foo => foo.Restore()).Verifiable();

            var deivce = deviceMock.Object;


            var zoneMock = new Mock<Zone>();
            zoneMock.Setup(foo => foo.InZone(It.IsAny<Person>())).Returns(true);

            var zone = zoneMock.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> {deivce},
                Identifier = "mock1",
                Zone = zone
            });

            pac.ActOnPeoplePresent(new List<Person>{ new Person() });

            deviceMock.Verify(t => t.Restore(), Times.Once);
            deviceMock.Verify(t => t.Off(), Times.Never);
            deviceMock.Verify(t => t.On(), Times.Never);
        }

        [TestMethod]
        public void PacActOnPerson_OnePerson_ZoneFalse()
        {
            var deviceMock = new Mock<IDevice>();

            deviceMock.Setup(foo => foo.On()).Verifiable();
            deviceMock.Setup(foo => foo.Off()).Verifiable();
            deviceMock.Setup(foo => foo.Restore()).Verifiable();

            var deivce = deviceMock.Object;


            var zoneMock = new Mock<Zone>();
            zoneMock.Setup(foo => foo.InZone(It.IsAny<Person>())).Returns(false);

            var zone = zoneMock.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce },
                Identifier = "mock1",
                Zone = zone
            });

            pac.ActOnPeoplePresent(new List<Person>{new Person()});

            deviceMock.Verify(t => t.Restore(), Times.Never);
            deviceMock.Verify(t => t.Off(), Times.Once);
            deviceMock.Verify(t => t.On(), Times.Never);
        }

        [TestMethod]
        public void PacActOnPerson_TwoPerson_DifferentZones()
        {
            var deviceMock1 = new Mock<IDevice>();
            deviceMock1.Setup(foo => foo.On()).Verifiable();
            deviceMock1.Setup(foo => foo.Off()).Verifiable();
            deviceMock1.Setup(foo => foo.Restore()).Verifiable();
            var deivce1 = deviceMock1.Object;

            var deviceMock2 = new Mock<IDevice>();
            deviceMock2.Setup(foo => foo.On()).Verifiable();
            deviceMock2.Setup(foo => foo.Off()).Verifiable();
            deviceMock2.Setup(foo => foo.Restore()).Verifiable();
            var deivce2 = deviceMock2.Object;


            var p1 = new Person();
            var p2 = new Person();

            var zoneMock1 = new Mock<Zone>();
            zoneMock1.Setup(foo => foo.InZone(p1)).Returns(false);
            zoneMock1.Setup(foo => foo.InZone(p2)).Returns(true);
            var zone1 = zoneMock1.Object;

            var zoneMock2 = new Mock<Zone>();
            zoneMock2.Setup(foo => foo.InZone(p1)).Returns(true);
            zoneMock2.Setup(foo => foo.InZone(p2)).Returns(false);
            var zone2 = zoneMock2.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce1 },
                Identifier = "mock1",
                Zone = zone1
            });

            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce2 },
                Identifier = "mock2",
                Zone = zone2
            });

            pac.ActOnPeoplePresent(new List<Person> { p1, p2 });

            deviceMock1.Verify(t => t.Restore(), Times.Once);
            deviceMock1.Verify(t => t.Off(), Times.Never);
            deviceMock1.Verify(t => t.On(), Times.Never);

            deviceMock2.Verify(t => t.Restore(), Times.Once);
            deviceMock2.Verify(t => t.Off(), Times.Never);
            deviceMock2.Verify(t => t.On(), Times.Never);
        }

        [TestMethod]
        public void PacActOnPerson_TwoPerson_SameZones()
        {
            var deviceMock1 = new Mock<IDevice>();
            deviceMock1.Setup(foo => foo.On()).Verifiable();
            deviceMock1.Setup(foo => foo.Off()).Verifiable();
            deviceMock1.Setup(foo => foo.Restore()).Verifiable();
            var deivce1 = deviceMock1.Object;

            var deviceMock2 = new Mock<IDevice>();
            deviceMock2.Setup(foo => foo.On()).Verifiable();
            deviceMock2.Setup(foo => foo.Off()).Verifiable();
            deviceMock2.Setup(foo => foo.Restore()).Verifiable();
            var deivce2 = deviceMock2.Object;


            var p1 = new Person();
            var p2 = new Person();

            var zoneMock1 = new Mock<Zone>();
            zoneMock1.Setup(foo => foo.InZone(p1)).Returns(true);
            zoneMock1.Setup(foo => foo.InZone(p2)).Returns(true);
            var zone1 = zoneMock1.Object;

            var zoneMock2 = new Mock<Zone>();
            zoneMock2.Setup(foo => foo.InZone(p1)).Returns(false);
            zoneMock2.Setup(foo => foo.InZone(p2)).Returns(false);
            var zone2 = zoneMock2.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce1 },
                Identifier = "mock1",
                Zone = zone1
            });

            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce2 },
                Identifier = "mock2",
                Zone = zone2
            });

            pac.ActOnPeoplePresent(new List<Person> { p1, p2 });

            deviceMock1.Verify(t => t.Restore(), Times.Once);
            deviceMock1.Verify(t => t.Off(), Times.Never);
            deviceMock1.Verify(t => t.On(), Times.Never);

            deviceMock2.Verify(t => t.Restore(), Times.Never);
            deviceMock2.Verify(t => t.Off(), Times.Once);
            deviceMock2.Verify(t => t.On(), Times.Never);
        }

        [TestMethod]
        public void PacActOnPerson_TwoPerson_InNoZones()
        {
            var deviceMock1 = new Mock<IDevice>();
            deviceMock1.Setup(foo => foo.On()).Verifiable();
            deviceMock1.Setup(foo => foo.Off()).Verifiable();
            deviceMock1.Setup(foo => foo.Restore()).Verifiable();
            var deivce1 = deviceMock1.Object;

            var deviceMock2 = new Mock<IDevice>();
            deviceMock2.Setup(foo => foo.On()).Verifiable();
            deviceMock2.Setup(foo => foo.Off()).Verifiable();
            deviceMock2.Setup(foo => foo.Restore()).Verifiable();
            var deivce2 = deviceMock2.Object;


            var p1 = new Person();
            var p2 = new Person();

            var zoneMock1 = new Mock<Zone>();
            zoneMock1.Setup(foo => foo.InZone(p1)).Returns(false);
            zoneMock1.Setup(foo => foo.InZone(p2)).Returns(false);
            var zone1 = zoneMock1.Object;

            var zoneMock2 = new Mock<Zone>();
            zoneMock2.Setup(foo => foo.InZone(p1)).Returns(false);
            zoneMock2.Setup(foo => foo.InZone(p2)).Returns(false);
            var zone2 = zoneMock2.Object;

            var pac = new Pac.Pac();
            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce1 },
                Identifier = "mock1",
                Zone = zone1
            });

            pac.AddSituation(new Scenario
            {
                Devices = new List<IDevice> { deivce2 },
                Identifier = "mock2",
                Zone = zone2
            });

            pac.ActOnPeoplePresent(new List<Person> { p1, p2 });

            deviceMock1.Verify(t => t.Restore(), Times.Never);
            deviceMock1.Verify(t => t.Off(), Times.Once);
            deviceMock1.Verify(t => t.On(), Times.Never);

            deviceMock2.Verify(t => t.Restore(), Times.Never);
            deviceMock2.Verify(t => t.Off(), Times.Once);
            deviceMock2.Verify(t => t.On(), Times.Never);
        }
    }
}
