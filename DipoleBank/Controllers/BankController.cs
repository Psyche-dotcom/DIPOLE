using DipoleBank.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using System.Security.Claims;

namespace DipoleBank.Controllers
{

    [Route("api/bank")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> Createbank(CreateBankDto bank)
        {

            var result = await _bankService.CreateBank(bank);
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
