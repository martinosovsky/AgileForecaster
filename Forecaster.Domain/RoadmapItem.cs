namespace Forecaster.Domain
{
    public class RoadmapItem
    {
        private Epic epic;
        private DateOnly finishDate;

        public RoadmapItem(Epic epic, DateOnly finishDate)
        {
            this.epic = epic;   
            this.finishDate = finishDate;   
        }


        public DateOnly FinishDate => finishDate;

        public override string ToString()
        {
            return $"{epic} will be finished by {finishDate.ToShortDateString()}.";
        }
    }
}