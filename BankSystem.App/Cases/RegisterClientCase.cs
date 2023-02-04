using System;
using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases
{
    public class RegisterClientCase
    {
        // ReSharper disable once NotAccessedField.Local
        private IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public RegisterClientCase(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public ClientResponse Get(Guid clientId)
        {
            var client = _clientRepository.Get(clientId);
            if (client == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {clientId} не зарегистрирован в системе.");
            }

            var mappedEmployee = _mapper.Map(client, new ClientResponse());

            return mappedEmployee;
        }

        public Guid AddClient(ClientRequest client)
        {
            var mappedClient = _mapper.Map<Client>(client);

            _clientRepository.Add(mappedClient);
            _clientRepository.Save();

            return mappedClient.Id;
        }

        public void DeleteClent(Guid clientId)
        {

        }

        public void UpdateClient(ClientRequest client)
        {

        }
    }
}
