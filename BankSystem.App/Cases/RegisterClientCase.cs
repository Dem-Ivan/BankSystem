using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using BankSystem.Domain.Validators;
using FluentValidation;

namespace BankSystem.App.Cases;

public class RegisterClientCase
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private ClientValidator _clientValidator;

    public RegisterClientCase(IUnitOfWork unitOfWork, IMapper mapper, ClientValidator clientValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _clientValidator = clientValidator;
    }

    public async Task<ClientResponse> Get(Guid clientId)
    {
        var client = await _unitOfWork.Clients.GetAsync(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {clientId} не зарегистрирован в системе.");
        }

        var mappedClient = _mapper.Map(client, new ClientResponse());

        return mappedClient;
    }

    public async Task<Guid> AddClient(ClientRequest client)
    {
        var mappedClient = _mapper.Map<Client>(client);
        _clientValidator.ValidateAndThrow(mappedClient);

        mappedClient.CreationDate = DateTime.UtcNow.Date;

        await _unitOfWork.Clients.AddAsync(mappedClient);
        await _unitOfWork.SaveAsync();

        return mappedClient.Id;
    }
}