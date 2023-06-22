using AutoMapper;
using Data.Repository.Interface;
using DipoleBank.Service.Interface;
using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Implementation
{
    public class BankService : IBankService
    {
        private readonly IBankRepo _bankRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<BankService> _logger;

        public BankService(IBankRepo bankRepo, IMapper mapper, ILogger<BankService> logger)
        {
            _bankRepo = bankRepo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ResponseDto<Bank>> CreateBank(CreateBankDto bank)
        {
            var response = new ResponseDto<Bank>();
            try
            {
                var mappedbank = _mapper.Map<Bank>(bank);
                var result = await _bankRepo.Createbank(mappedbank);
                if (result == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in creating bank" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in creating bank" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
