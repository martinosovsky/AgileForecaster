using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Forecaster.Domain.Tests
{
    [TestClass]
    public class EpicTests
    {
        [DataTestMethod]
        [DataRow(1, 1, 1, 1)]
        [DataRow(2, 1, 2, 1)]
        [DataRow(10, 10, 10, 10)]
        public void TestMethod1(int velocity, int sprintLength, int storyPoints, int expected)
        {
            var epic = new Epic("id", "summary", storyPoints);

            int actual = epic.GetDays(velocity, sprintLength);

            actual.Should().Be(expected);

        }

        [TestMethod]
        public void CountDays_ZeroSprintLengthThrowsArgumentException()
        {
            var epic = new Epic("id", "summary", sp: 21);

            Fixture? fixture = new Fixture();

            int velocity = fixture.Create<int>();

            Assert.ThrowsException<ArgumentException>(() => epic.GetDays(velocity, 0));

        }

        [TestMethod]
        public void CountDays_ZeroVelocityThrowsArgumentException()
        {
            var epic = new Epic("id", "summary", sp: 21);

            Fixture? fixture = new Fixture();

            int sprintLength = fixture.Create<int>();

            Assert.ThrowsException<ArgumentException>(() => epic.GetDays(0, sprintLength));

        }

        [TestMethod]
        public void ToString_RendersRight()
        {
            var epic = new Epic("id", "summary", sp: 21);

            epic.ToString().Should().Be("id: summary (21)");
        }


    }
}