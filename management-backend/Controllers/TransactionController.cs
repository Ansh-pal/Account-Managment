using AccountManager.API.DTOs;
using AccountManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(TransferDto dto)
        {
            var result = _transactionService.Transfer(dto);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpGet("account/{accountId}")]
        public IActionResult GetAccountTransactions(int accountId)
        {
            var transactions = _transactionService.GetAccountTransactions(accountId);
            return Ok(transactions);
        }
    }
}
