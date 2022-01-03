using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Forecaster.Domain.Tests
{

    [TestClass]
    public class RoadmapTests
    {
        [TestMethod]
        public void FirstRoadmapTest()
        {
            var backlog = new Backlog();

            var roadmap = new Roadmap();

            roadmap.Should().BeOfType<Roadmap>();
        }


        //TODO: This should not be the responsibility of either Backlog or Roadmap, create a strategy
        [TestMethod]
        public void Ctr_GivenBacklogAndStartDate_Calculates()
        {
            var fixture = new Fixture();
            var epics = fixture.CreateMany<Epic>().ToList();

            var backlog = new Backlog(epics);

            var roadmap = backlog.Calculate(DateTime.Now);

            roadmap.Should().NotBeNull();
        }

        [TestMethod]
        public void GetNth_GivenEmpty_Throws()
        {
            var roadmap = new Roadmap();

            roadmap.Invoking(r => r.GetNthItem(1)).Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetNth_GivenNonEpty_ReturnsFirstRight()
        {
            var fixture = new Fixture();

            var epics = fixture.CreateMany<Epic>().ToList();
            var roadmap = Roadmap.CalculateRoadmap(new Backlog(epics), new FakeStrategy());

            roadmap.GetNthItem(1).FinishDate.Should().BeEquivalentTo(new DateOnly(2022,1,1));
        }

        [TestMethod]
        public void Items_GivenNonEmpty_CanBeEnumerated()
        {
            var fixture = new Fixture();

            var epics = fixture.CreateMany<Epic>().ToList();
            var roadmap = Roadmap.CalculateRoadmap(new Backlog(epics), new FakeStrategy());

            foreach (var item in roadmap.Items)
            {
                item.FinishDate.Should().BeEquivalentTo(new DateOnly(2022,1,1));
            }
        }

        [TestMethod]
        public void ToString_GivenFirstItemOfNonEmpty_RendersRight()
        {
            var fixture = new Fixture();

            var epics = fixture.CreateMany<Epic>().ToList();
            var roadmap = Roadmap.CalculateRoadmap(new Backlog(epics), new FakeStrategy());

            roadmap.GetNthItem(1).ToString().Should().Be($"{epics[0]} will be finished by 1/1/2022.");
        }
    }

    internal class FakeStrategy : IForecastingStrategy
    {
        public Roadmap Calculate(Backlog backlog)
        {
            var roadmap = new Roadmap();
            foreach (var epic in backlog.EnumerateEpics())
            {
                roadmap.Add(new RoadmapItem(epic, new DateOnly(2022,1,1)));
            }

            return roadmap;
        }
    }
}