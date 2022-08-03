using System;

namespace AcmePayAssessment.DataAccessLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        AcmeDbContext Context { get; }
        void Commit();
    }
}
