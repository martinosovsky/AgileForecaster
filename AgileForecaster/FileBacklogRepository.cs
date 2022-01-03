using Forecaster.Domain;
using Forecaster.UseCases;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileForecaster
{
    internal class FileBacklogRepository : IRepository<Backlog>
    {
        private string fileName;

        public FileBacklogRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public async Task<Backlog> GetNewestBacklog(DateTime now)
        {
            string text = await File.ReadAllTextAsync(fileName);

            string[] lines = text.Split(new char[] { '\n' });

            List<Epic> epicList = new List<Epic>();

            foreach (var line in lines)
            {
                var elements = line.Split(",");
                if (elements.Length != 3)
                {
                    throw new Exception("Wrong file format!");
                }

                int storyPoints = 0;
                Int32.TryParse(elements[2], out storyPoints);

                var epic = new Epic(elements[0], elements[1], storyPoints);
                epicList.Add(epic);

            }


            return new Backlog(epicList.ToArray());

        }
    }
}
