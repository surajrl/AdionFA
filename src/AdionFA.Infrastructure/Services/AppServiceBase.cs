using AdionFA.Infrastructure.Persistance.Contracts;
using AutoMapper;
using Ninject;
using System;

namespace AdionFA.Application.Services
{
    public class AppServiceBase
    {
        [Inject]
        public ITransaction Transactional { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        // Data

        public IDisposable Transaction<T>()
        {
            return Transactional.Transactional<T>();
        }

        public void Dispose()
        {
            Transactional.ReleaseDbContext();
        }
    }
}
