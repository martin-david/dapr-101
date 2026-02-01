using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace state_management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private const string STATESTORE_NAME = "statestore";

        [HttpGet("{accountNumber}")]
        public decimal Get([FromState(STATESTORE_NAME, "accountNumber")] int balance) => balance;

        [HttpPost("{accountNumber}/{amount}")]
        public async Task<decimal> Deposit([FromState(STATESTORE_NAME, "accountNumber")] StateEntry<decimal> balance, decimal amount)
        {
            balance.Value += amount;
            await balance.SaveAsync();
            return balance.Value;
        }

        [HttpDelete("{accountNumber}/{amount}")]
        public async Task<decimal> Withdraw([FromState(STATESTORE_NAME, "accountNumber")] StateEntry<decimal> balance, decimal amount)
        {
            balance.Value -= amount;
            await balance.SaveAsync();
            return balance.Value;
        }
    }
}
