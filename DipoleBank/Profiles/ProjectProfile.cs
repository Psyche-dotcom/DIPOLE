using AutoMapper;
using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ApplicationUser, DisplayFindUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, SignUp>()
                .ForMember(dest => dest.AppKey, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmAppKey, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ApplicationUser, UpdateUserDto>().ReverseMap();
            CreateMap<CreateBankDto, Bank>().ReverseMap();
            CreateMap<CreateBankAccountDto, Account>().ReverseMap();
            CreateMap<DisplayAccountDto, Account>().ReverseMap();
        }
    }
}
