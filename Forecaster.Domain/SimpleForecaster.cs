using System;


namespace Forecaster.Domain
{
    public class SimpleForecaster : IForecastingStrategy
    {
        private int velocity;
        private int sprintLenth;
        private DateOnly startDate;

        public SimpleForecaster(int velocity, int sprintLenth, DateOnly startDate)
        {
            this.velocity = velocity;
            this.sprintLenth = sprintLenth;
            this.startDate = startDate;
        }

        Roadmap IForecastingStrategy.Calculate(Backlog backlog)
        {
            if (backlog is null)
            {
                throw new ArgumentNullException(nameof(backlog));
            }

            var roadmap = new Roadmap();
            var newStartDate = startDate;

            foreach (var epic in backlog.EnumerateEpics())
            {
                DateOnly finishDate = CalculateFinishDate(newStartDate, epic);
                var newItem = new RoadmapItem(epic, finishDate);

                roadmap.Add(newItem);
                newStartDate = finishDate;
            }

            return roadmap;
        }

        private DateOnly CalculateFinishDate(DateOnly newStartDate, Epic epic)
        {
            int days = epic.GetDays(velocity, sprintLenth);
            var finishDate = newStartDate.AddDays(days);
            return finishDate;
        }
    }
}