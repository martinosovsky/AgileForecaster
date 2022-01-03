using System;
using Forecaster.Domain;
using System.Threading.Tasks;

namespace Forecaster.UseCases
{
    public class GetForecastUseCase
    {

        private IRepository<Backlog> _backlogRepository;
        private IOutputPort<Roadmap> _outputPort;
        private IForecastingStrategy _forecastingStrategy;

        public GetForecastUseCase(IRepository<Backlog> backlogRepository, IOutputPort<Roadmap> outputPort, IForecastingStrategy strategy)
        {
            _backlogRepository = backlogRepository;
            _outputPort = outputPort;
            _forecastingStrategy = strategy;
        }

        public async Task Execute()
        {
            await GetNewRoadmap();
        }

        private async Task GetNewRoadmap()
        {
            // get data
            var backlog = await _backlogRepository.GetNewestBacklog(DateTime.Now);

            // validate
            if (backlog == null || backlog.CountEpics() == 0)
            {
                _outputPort.Fail("No latest backlog found or backlog empty.");
                return;
            }

            // run a business process
            var roadmap = Roadmap.CalculateRoadmap(backlog, _forecastingStrategy);
            _outputPort.Ok(roadmap);

        }
    }
}