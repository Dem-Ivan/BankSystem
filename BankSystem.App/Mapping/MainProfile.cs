using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.Domain.Models;

namespace BankSystem.App.Mapping
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<EmployeeRequest, Employee>(MemberList.Source);
            CreateMap<Employee, EmployeeResponse>(MemberList.Source);

            CreateMap<Contract, ContractResponse>()
                .ForMember(x => x.Status, exp => exp.MapFrom(x => x.Status))
                .ForMember(x => x.Id, exp => exp.MapFrom(x => x.Id))
                ;

        }
    }
}
