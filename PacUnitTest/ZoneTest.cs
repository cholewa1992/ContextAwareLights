﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pac;
using Pac.Model;

namespace PacUnitTest
{
    [TestClass]
    public class ZoneTest
    {

        private Beacon b1 = new Beacon
        {
            Distance = 2,
            Major = 5000,
            Minor = 1921
        };

        private Beacon b1Real = new Beacon
        {
            Distance = 1.41245125,
            Major = 5000,
            Minor = 1921
        };

        private Beacon b2 = new Beacon
        {
            Distance = 4,
            Major = 5000,
            Minor = 1922
        };

        private Beacon b2Real = new Beacon
        {
            Distance = 1.9080980932,
            Major = 5000,
            Minor = 1922
        };

        private Beacon b3close = new Beacon
        {
            Distance = 1,
            Major = 5000,
            Minor = 1923
        };

        private Beacon b3 = new Beacon
        {
            Distance = 6,
            Major = 5000,
            Minor = 1923
        };

        private Beacon b3thres = new Beacon
        {
            Distance = 6.2,
            Major = 5000,
            Minor = 1923
        };

        private Beacon b3Real = new Beacon
        {
            Id = Guid.NewGuid(),
            Distance = 4.192874,
            Major = 5000,
            Minor = 1923
        };

  

        [TestMethod]
        public void InZoneTrue()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real, b2Real }
            };

            var zone = new Zone
            {
                Signature = new List<Beacon> { b1, b2 }
            };

            Assert.IsTrue(zone.InZone(person));

        }

        [TestMethod]
        public void InZoneFalse()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real }
            };

            var zone = new Zone
            {
                Signature = new List<Beacon> { b1, b2 }
            };

            Assert.IsFalse(zone.InZone(person));
        }

        [TestMethod]
        public void InZoneWithAccuracyTrue()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real }
            };

            var zone = new Zone
            {
                Accuracy = 0.5d,
                Signature = new List<Beacon> { b1, b2 }
            };

            Assert.IsTrue(zone.InZone(person));
        }

        [TestMethod]
        public void InZoneWithAccuracyFalse()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real }
            };

            var zone = new Zone
            {
                Accuracy = 0.5d,
                Signature = new List<Beacon> { b1, b2, b3 }
            };

            Assert.IsFalse(zone.InZone(person));
        }

        [TestMethod]
        public void InZoneWithinThreshold()
        {
            var person = new Person()
            {
                Beacons = new[] {b3close}
            };

            var zone = new Zone()
            {
                Signature = new List<Beacon>() {b3}
            };

            Assert.IsFalse(zone.InZone(person));

            zone.InZone(person);

            person.Beacons = new[] {b3thres};

            
        }

        [TestMethod]
        public void OutZoneOutOfThreshold()
        {
            var person = new Person()
            {
                Beacons = new[] { b3thres }
            };

            var zone = new Zone()
            {
                Signature = new List<Beacon>() { b3 }
            };

            Assert.IsFalse(zone.InZone(person));
        }

        public void InZoneWithExcludeTrue()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real, b3Real }
            };

            var zone = new Zone
            {
                Signature = new List<Beacon> { b1, b3 },
                Exclude = new List<Beacon> { b2 }
            };

            Assert.IsTrue(zone.InZone(person));
        }


        [TestMethod]
        public void InZoneWithExcludeFalse()
        {
            var person = new Person()
            {
                Beacons = new[] { b1Real,b2Real, b3Real }
            };

            var zone = new Zone
            {
                Signature = new List<Beacon> { b1, b3 },
                Exclude = new List<Beacon> { b2 }
            };

            Assert.IsFalse(zone.InZone(person));
        }
    }
}
