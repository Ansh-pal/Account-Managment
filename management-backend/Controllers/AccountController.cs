using AccountManager.API.DTOs;
using AccountManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] CreateAccountDto dto)
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized("User id claim is missing or invalid.");
            }

            var account = _accountService.CreateAccount(userId, dto);
            return Ok(account);
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            if (!TryGetUserId(out var userId))
            {
                return Unauthorized("User id claim is missing or invalid.");
            }

            var accounts = _accountService.GetAccounts(userId);
            return Ok(accounts);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            _accountService.DeleteAccount(id);
            return Ok("Account deleted");
        }

        private bool TryGetUserId(out int userId)
        {
            var idClaim = User.FindFirstValue("id");
            return int.TryParse(idClaim, out userId);
        }
    }
}