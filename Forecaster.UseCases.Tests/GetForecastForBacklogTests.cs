using NUnit.Framework;
using System;
using Forecaster.Domain;
using FluentAssertions;
using System.Threading.Tasks;

namespace Forecaster.UseCases.Tests
{
    public class GetForecastForBacklogTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetNewForecastTest()
        {
            var outPort = new FakePresenter();

            var getForecastUseCase = new GetForecastUseCase(new FakeBacklogRepository(empty: false), outPort, new TrivialForecaster());

            Task expected = getForecastUseCase.Execute();

            expected.Status.Should().Be(TaskStatus.RanToCompletion);
        }

        [Test]
        public void Execute_ShouldFail_ifRoadmapEmpty()
        {
            var outPort = new FakePresenter();

            var getForecastUseCase = new GetForecastUseCase(new FakeBacklogRepository(empty: true), outPort, new TrivialForecaster());

            getForecastUseCase.Execute().Wait();

            outPort.IsOk.Should().BeFalse();
        }

        [Test]
        public void Execute_GivenNonEmptyBacklog_CallsOK()
        {
            var outPort = new FakePresenter();

            var getForecastUseCase = new GetForecastUseCase(new FakeBacklogRepository(empty: false), outPort, new TrivialForecaster());

            getForecastUseCase.Execute().Wait();

            outPort.IsOk.Should().BeTrue();
        }

        [Test]
        public void Execute_GivenNonEmptyBacklog_ReturnsExpectedRoadmap()
        {
            var outPort = new FakePresenter();
            var forecaster = new SimpleForecaster(10, 10, new DateOnly(2022, 1, 1));

            var getForecastUseCase = new GetForecastUseCase(new FakeBacklogRepository(empty: false), outPort, forecaster);

            getForecastUseCase.Execute().Wait();

            outPort.Roadmap.GetNthItem(1).FinishDate.ToShortDateString().Should().Be("1/11/2022");
            outPort.Roadmap.GetNthItem(2).FinishDate.ToShortDateString().Should().Be("1/31/2022");
        }


    }

    internal class TrivialForecaster : IForecastingStrategy
    {
        public Roadmap Calculate(Backlog backlog)
        {
            return new Roadmap();
        }
    }
}
