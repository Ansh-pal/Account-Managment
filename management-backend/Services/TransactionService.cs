using AccountManager.API.Data;
using AccountManager.API.DTOs;
using AccountManager.API.Models;

namespace AccountManager.API.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Transfer(TransferDto dto)
        {
            if (dto.Amount <= 0)
            {
                return "Transfer amount must be greater than zero";
            }

            var fromAccount = _context.Accounts.FirstOrDefault(a => a.Id == dto.FromAccountId);
            var toAccount = _context.Accounts.FirstOrDefault(a => a.Id == dto.ToAccountId);

            if (fromAccount == null || toAccount == null)
            {
                return "One or both accounts were not found";
            }

            if (fromAccount.Balance < dto.Amount)
            {
                return "Insufficient balance";
            }

            fromAccount.Balance -= dto.Amount;
            toAccount.Balance += dto.Amount;

            _context.Transactions.Add(new Transaction
            {
                FromAccountId = dto.FromAccountId,
                ToAccountId = dto.ToAccountId,
                Amount = dto.Amount
            });

            _context.SaveChanges();

            return "Transfer successful";
        }

        public List<Transaction> GetTransactions()
        {
            return _context.Transactions
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public List<Transaction> GetAccountTransactions(int accountId)
        {
            return _context.Transactions
                .Where(t => t.FromAccountId == accountId || t.ToAccountId == accountId)
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }
    }
}
