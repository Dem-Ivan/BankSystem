using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.Domain.Models;

namespace BankSystem.App.Mapping
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Contract, ContractResponse>();
        }
    }
}
