namespace Forecaster.Domain
{
    public interface IForecastingStrategy
    {
        Roadmap Calculate(Backlog backlog);
    }
}