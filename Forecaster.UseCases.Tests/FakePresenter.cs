using Forecaster.Domain;
using System;

namespace Forecaster.UseCases.Tests
{
    internal enum Status
    {
        Na,
        Ok,
        Failed
    }

    internal class FakePresenter : IOutputPort<Roadmap>
    {
        private string message;
        private Status status;

        public Roadmap Roadmap { get; private set; }

        public FakePresenter()
        {
            this.message = string.Empty;
            this.Roadmap = new Roadmap();
        }

        public bool IsOk => status == Status.Ok;

        bool IOutputPort<Roadmap>.Ok(Roadmap roadmap)
        {
            message = $"{roadmap}";
            Roadmap = roadmap;
            status = Status.Ok;

            return true;
        }

        public void Fail(string errorMessage)
        {
            message = errorMessage;
            status = Status.Failed;
        }

        public string Render()
        {
            if (message == null)
            {
                throw new ArgumentException("message");
            }

            if (message == string.Empty)
            {
                throw new InvalidOperationException("There is nothing to render.");
            }
            return message;
        }
        
    }
}