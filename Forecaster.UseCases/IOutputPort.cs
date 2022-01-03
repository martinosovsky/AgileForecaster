using Forecaster.Domain;

namespace Forecaster.UseCases
{
    public interface IOutputPort<in T>
    {
        bool Ok(T roadmap);
        void Fail(string errorMessage);
    }
}