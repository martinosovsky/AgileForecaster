using System;

namespace Forecaster.Domain
{
    public class Epic
    {
        private string id;
        private string summary;
        private int sp;



        public Epic(string id, string summary, int sp)
        {
            this.id = id;
            this.summary = summary;
            this.sp = sp;
        }

        public int GetDays(int velocity, int sprintLength)
        {
            if (sprintLength == 0)
            {
                throw new ArgumentException("sprintLength");
            }
            if (velocity == 0)
            {
                throw new ArgumentException("velocity");
            }

            return  (sprintLength * sp)  / velocity;
        }

        public override string ToString()
        {
            return $"{id}: {summary} ({sp})";
        }
    }
}