using AccountManager.API.Data;
using AccountManager.API.DTOs;
using AccountManager.API.Models;

namespace AccountManager.API.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account CreateAccount(int userId, CreateAccountDto dto)
        {
            var account = new Account
            {
                UserId = userId,
                AccountName = dto.AccountName,
                Balance = dto.InitialBalance
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        public List<Account> GetAccounts(int userId)
        {
            return _context.Accounts
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
        }

        public void DeleteAccount(int id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return;
            }

            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
