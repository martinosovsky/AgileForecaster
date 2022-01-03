using Forecaster.UseCases;
using Forecaster.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileForecaster
{
    internal class CLIPresenterAndView : IOutputPort<Roadmap>
    {
        string message = "Nothing was done.";
        List<string> roadmapItems = new List<string>();

        void IOutputPort<Roadmap>.Fail(string errorMessage)
        {
            message = $"Getting roadmap failed due to error:{errorMessage}";
        }

        //TODO: refactor Ok not to return bool (duh)
        bool IOutputPort<Roadmap>.Ok(Roadmap roadmap)
        {
            message = "Roadmap was succesfully calculated.";
            foreach (var item in roadmap.Items)
            {
                roadmapItems.Add(item.ToString());
            }

            return true;
        }

        public void Render()
        {
            Console.WriteLine(message);
            foreach (var item in roadmapItems)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("".PadLeft(roadmapItems.Max(s => s.Length), 'X').Replace("X", "*"));
        }
    }
}
