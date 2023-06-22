using DipoleBank.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;

namespace DipoleBank.Controllers
{
    [Route("api/bank/account")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {

        private readonly IBankAccountService _bankAccountService;

        public BankAccountController(IBankAccountService bankAccountService)
        {

            _bankAccountService = bankAccountService;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateBankAccount(CreateBankAccountDto bankaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bankAccountService.CreateBankAccount(bankaccount);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
