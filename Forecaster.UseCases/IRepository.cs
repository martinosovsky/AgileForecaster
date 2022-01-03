using Forecaster.Domain;
using System;
using System.Threading.Tasks;

namespace Forecaster.UseCases
{
    public interface IRepository<T>
    {
        Task<Backlog> GetNewestBacklog(DateTime now);
    }
}