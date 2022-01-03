using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forecaster.Domain;
using AutoFixture;

namespace Forecaster.Domain.Tests
{
    [TestClass]
    public class BacklogTests
    {

        Backlog _nonEmptyBacklog = new Backlog();
        static Backlog _emptyBacklog = new Backlog();

        [TestInitialize]
        public void TestSetup()
        {
            var fixture = new Fixture();
            var epics = fixture.CreateMany<Epic>().ToList();

            _nonEmptyBacklog = new Backlog(epics);
        }

        [TestMethod]
        public void BacklogIsCreatedNonNull()
        {
            var backlog = new Backlog();

            backlog.Should().BeOfType<Backlog>();
        }

        [TestMethod]
        public void Count_ReturnsNumberOfEpics()
        {
            var fixture = new Fixture();
            var epics = fixture.CreateMany<Epic>().ToList();

            var backlog = new Backlog(epics);

            backlog.CountEpics().Should().Be(epics.Count);
        }

        [TestMethod]
        public void Count_WhenEmpty_Returns0()
        {
            var backlog = new Backlog();

            backlog.CountEpics().Should().Be(0);
        }

        [TestMethod]
        public void GetNth_ThrowsIfNegative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _nonEmptyBacklog.GetNth(-1));
        }

        [TestMethod]
        public void GetNth_ThrowsIfMorethanCount()
        {
            int count = _nonEmptyBacklog.CountEpics();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _nonEmptyBacklog.GetNth(count + 1));
        }

        [TestMethod]
        public void GetNth_GivenCount_ReturnsLast()
        {
            var lastEpic = _nonEmptyBacklog.GetNth(_nonEmptyBacklog.CountEpics());

            lastEpic.Should().NotBeNull();
            _nonEmptyBacklog.Invoking(b => b.GetNth(_nonEmptyBacklog.CountEpics() + 1)).Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void GetNth_Given1_ReturnsFirst()
        {
            var lastEpic = _nonEmptyBacklog.GetNth(1);

            lastEpic.Should().NotBeNull();
        }

        [TestMethod]
        public void Items_CorrectlyEnumerate()
        {
            foreach (var epic in _nonEmptyBacklog.EnumerateEpics())
            {
                epic.Should().NotBeNull();
            }

        }

    }
}
