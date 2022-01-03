namespace Forecaster.Domain
{
    public class Roadmap
    {
        private IList<RoadmapItem> items;

        public Roadmap()
        {
            items = new List<RoadmapItem>();
        }

        public void Add(RoadmapItem item)
        {
            items.Add(item);
        }

        public IEnumerable<RoadmapItem> Items => items.AsEnumerable();

        public static Roadmap CalculateRoadmap(Backlog backlog, IForecastingStrategy forecaster)
        {
            return forecaster.Calculate(backlog);
        }

        public RoadmapItem GetNthItem(int v)
        {
            return items[v - 1];
        }
    }
}