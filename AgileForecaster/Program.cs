using Forecaster.Domain;
using Forecaster.UseCases;
using AgileForecaster;


// See https://aka.ms/new-console-template for more information

string fileName = @".\BacklogSource.txt";

Console.WriteLine($"Reading Input from {fileName}");

var epicRepository = new FileBacklogRepository(fileName);
var cliPresenter = new CLIPresenterAndView();
var forecastingStrategy = new SimpleForecaster(10, 10, new DateOnly(2022, 1, 3));

var useCase = new GetForecastUseCase(epicRepository, cliPresenter, forecastingStrategy);

useCase.Execute().Wait();

cliPresenter.Render();