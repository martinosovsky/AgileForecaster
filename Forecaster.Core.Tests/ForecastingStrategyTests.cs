using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Forecaster.Domain.Tests
{
    [TestClass]
    public class ForecastingStrategyTests
    {
        private readonly static int sprintLength = 10;

        private static Backlog GetBacklog()
        {
            var epic1 = new Epic("1", "prvni epic", 10);
            var epic2 = new Epic("2", "druhy epic", 20);

           return new Backlog(new List<Epic> { epic1, epic2 });
        }


        [TestMethod]
        public void Test1()
        {
            var backlog = new Backlog();
            int velocity = 0;
            int sprintLength = 0;
            DateOnly startDate = new DateOnly(); 

            IForecastingStrategy forecaster = new SimpleForecaster(velocity, sprintLength, startDate);

            var roadmap = forecaster.Calculate(backlog);

            Assert.IsNotNull(roadmap);
        }


        [DataTestMethod]
        [DataRow(10, "1/11/2022")]
        public void Calculate_FirstEpicOK(int velocity, string expectedDate)
        {
            DateOnly startDate = new DateOnly(2022,1,1);

            IForecastingStrategy forecaster = new SimpleForecaster(velocity, sprintLength, startDate);

            var backlog = GetBacklog();

            var roadmap = forecaster.Calculate(backlog);

            roadmap.GetNthItem(1).FinishDate.ToShortDateString().Should().Be(expectedDate);
        }

        [DataTestMethod]
        [DataRow(10, "1/31/2022")]
        public void Calculate_LastEpicOK(int velocity, string expectedDate)
        {
            DateOnly startDate = new DateOnly(2022, 1, 1);

            IForecastingStrategy forecaster = new SimpleForecaster(velocity, sprintLength, startDate);

            var backlog = GetBacklog();

            var roadmap = forecaster.Calculate(backlog);

            roadmap.GetNthItem(2).FinishDate.ToShortDateString().Should().Be(expectedDate);
        }

    }
}
