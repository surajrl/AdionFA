using System;

namespace FacadePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var schoolService = FacadeService.SchoolAPI;

            schoolService.GetSchoolName();
            schoolService.GetSchoolName();
        }
    }

    #region Service

    abstract class ServiceBase<Serivce> : IDisposable where Serivce : ServiceBase<Serivce>
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ServiceBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    interface ISchoolAppService
    {
        public void GetSchoolName();
    }

    class SchooolAppService : ServiceBase<SchooolAppService>, ISchoolAppService
    {
        public void GetSchoolName()
        {
            Console.WriteLine("Rene Perez");
        }
    }

    #endregion

    #region Api

    interface ISchoolAPI
    {
        public void GetSchoolName();
    }

    class SchoolAPI : ISchoolAPI
    {
        private readonly ISchoolAppService _schooolAppService;

        public SchoolAPI()
        {
            _schooolAppService = new SchooolAppService();
        }

        public void GetSchoolName()
        {
            _schooolAppService.GetSchoolName();

        }
    }

    #endregion

    #region Facade

    static class FacadeService
    { 
        public static SchoolAPI SchoolAPI => new SchoolAPI();
    }

    #endregion
}