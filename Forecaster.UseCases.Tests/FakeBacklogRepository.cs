using Forecaster.Domain;
using System;
using System.Threading.Tasks;

namespace Forecaster.UseCases.Tests
{
    internal class FakeBacklogRepository : IRepository<Backlog>
    {
        Backlog _backlog;

        public FakeBacklogRepository(bool empty)
        {
            if (empty)
            {
                _backlog = new Backlog();
            }
            else
            {
                var epic1 = new Epic("1", "prvni epic", 10);
                var epic2 = new Epic("2", "druhy epic", 20);

                _backlog = new Backlog(new[] { epic1, epic2 });
            }
        }

        async Task<Backlog> IRepository<Backlog>.GetNewestBacklog(DateTime now)
        {
            return await Task.FromResult(_backlog).ConfigureAwait(false);
        }
    }
}