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

            CreateMap<Contract, ContractResponse>(MemberList.Source);               
        }
    }
}
