using examPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace examPattern.Interface
{
    public interface IInsuranceRepository : IDisposable
    {
        // Define repository methods for CRUD operations and queries
        // Example:
        void AddInsuranceProduct(InsuranceProduct product);
        void UpdateInsuranceProduct(InsuranceProduct product);
        InsuranceProduct GetInsuranceProductById(int id);
        // Other methods
        void SaveChanges();
    }

    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly InsuranceDbContext _context;

        public InsuranceRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        // Implement repository methods
        // Example:
        public void AddInsuranceProduct(InsuranceProduct product)
        {
            _context.InsuranceProducts.Add(product);
        }

        public void UpdateInsuranceProduct(InsuranceProduct product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        // Other methods

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public InsuranceProduct GetInsuranceProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        // Dispose method
    }

    public interface IUnitOfWork : IDisposable
    {
        IInsuranceRepository InsuranceRepository { get; }
        // Other repositories if needed
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly InsuranceDbContext _context;

        public UnitOfWork(InsuranceDbContext context)
        {
            _context = context;
            InsuranceRepository = new InsuranceRepository(_context);
            // Other repositories initialization if needed
        }

        public IInsuranceRepository InsuranceRepository { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        // Other repositories if needed

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        // Dispose method
    }

}
