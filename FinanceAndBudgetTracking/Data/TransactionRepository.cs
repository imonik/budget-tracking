using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Services;
using Microsoft.EntityFrameworkCore;

namespace FinanceAndBudgetTracking.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Transaction?> AddTransaction(Transaction transaction)
        {
            try
            {
                var newTransaction = await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return newTransaction.Entity;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not save the transaction to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<Transaction> DeleteTransaction(int transactionId)
        {
            try
            {
                var getExistingTransaction = await _context.Transactions.FindAsync(transactionId);
                if ( getExistingTransaction == null)
                {
                    throw new ApplicationException("Transaction not found");
                }
                _context.Transactions.Remove(getExistingTransaction);
                await _context.SaveChangesAsync();

                return getExistingTransaction;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not delete the transaction from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            try
            {
                var getExistingTransaction = await _context.Transactions.FindAsync(transactionId);
                if (getExistingTransaction == null)
                {
                    throw new ApplicationException("Transaction not found");
                }
                return getExistingTransaction;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not retrieve the transactions from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }
        public async Task<IEnumerable<Transaction>> GetTransactionsByRange(int id, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var transactionsByDate = await _context.Transactions.Where(t => t.Date >= startDate && t.Date <= endDate && t.UserId == id).ToListAsync();

                if (transactionsByDate == null)
                {
                    throw new ApplicationException("Transaction not found");
                }

                return transactionsByDate;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not retrieve the transactions from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByType(bool type)
        {
            try
            {
                var updtadetTransaction = await _context.Transactions.Where(t => t.Type == type).ToListAsync();
                if (updtadetTransaction == null)
                {
                    throw new ApplicationException("Transaction not found");
                }

                return updtadetTransaction;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not retrieve the transactions from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId)
        {
            try
            {
                var transactionList = await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
                if (transactionList == null)
                {
                    throw new ApplicationException("No transactions found");
                }
                return transactionList;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not retrieve the transactions from the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }

        public async Task<int> UpdateTransaction(Transaction transaction)
        {
            try
            {
                var transactionId = await _context.Transactions.Where(t => t.TransactionId == transaction.TransactionId)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.UserId, transaction.UserId)
                .SetProperty(p => p.Type, transaction.Type)
                .SetProperty(p => p.CategoryId, transaction.CategoryId)
                .SetProperty(p => p.Amount, transaction.Amount)
                .SetProperty(p => p.Date, transaction.Date)
                .SetProperty(p => p.Description, transaction.Description)
                .SetProperty(p => p.CreatedOn, transaction.CreatedOn)
                .SetProperty(p => p.ModifiedOn, DateTime.Now)
                );
                if (transactionId == 0)
                {
                    throw new Exception("Update failed");
                }
                return transactionId;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the exception or handle specific DB issues
                // e.g., _logger.LogError(dbEx, "Database update failed.");
                throw new ApplicationException("Could not date the transaction to the database.", dbEx);
            }
            catch (Exception ex)
            {
                // Catch other unexpected exceptions
                throw new ApplicationException("An unexpected error occurred while adding the transaction.", ex);
            }
        }
    }
}
