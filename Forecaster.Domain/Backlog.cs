namespace Forecaster.Domain
{
    public class Backlog
    {
        private IList<Epic> epics;

        public Backlog()
        {
            epics = new List<Epic>();
        }

        public Backlog(IList<Epic> epics)
        {
            this.epics = epics;
        }

        public int CountEpics()
        {
            return epics.Count;
        }

        public Epic GetNth(int v)
        {
            if (v < 0 || v > epics.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return epics[v - 1];
        }

        public object Calculate(DateTime now)
        {
            return new Roadmap();
        }

        public IEnumerable<Epic> EnumerateEpics()
        {
            return epics.AsEnumerable<Epic>();
        }
    }
}