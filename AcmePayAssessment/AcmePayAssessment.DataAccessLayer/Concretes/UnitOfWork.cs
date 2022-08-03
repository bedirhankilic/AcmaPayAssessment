using AcmePayAssessment.DataAccessLayer.Abstract;

namespace AcmePayAssessment.DataAccessLayer.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        public AcmeDbContext Context { get; }

        public UnitOfWork(AcmeDbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
