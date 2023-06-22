using AutoMapper;
using Data.Repository.Implementation;
using Data.Repository.Interface;
using DipoleBank.Service.Interface;
using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Implementation
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepo _bankAccountRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<BankAccountService> _logger;
        private readonly IUserRepo _userRepo;

        public BankAccountService(IBankAccountRepo bankAccountRepo, IMapper mapper, ILogger<BankAccountService> logger, IUserRepo userRepo)
        {
            _bankAccountRepo = bankAccountRepo;
            _mapper = mapper;
            _logger = logger;
            _userRepo = userRepo;
        }

        public async Task<ResponseDto<DisplayAccountDto>> CreateBankAccount(CreateBankAccountDto account)
        {
            var response = new ResponseDto<DisplayAccountDto>();
            try
            {

                var mappedbankAccount = _mapper.Map<Account>(account);
                var checkAppId = await _userRepo.CheckAppId(account.AppId);
                if(checkAppId == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid App Id" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                mappedbankAccount.UserId = checkAppId.Id;
                var result = await _bankAccountRepo.CreateAccount(mappedbankAccount);
                if (result == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in creating bank account" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mappResult = _mapper.Map<DisplayAccountDto>(result);
                mappResult.AccountName = checkAppId.FirstName + " " + checkAppId.LastName;
                mappResult.BankName = "DipoleDiamond";
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = mappResult;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating bank account" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
