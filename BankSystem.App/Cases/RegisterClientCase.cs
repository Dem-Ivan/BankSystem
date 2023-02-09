using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases;

public class RegisterClientCase
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterClientCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public ClientResponse Get(Guid clientId)
    {
        var client = _unitOfWork.Clients.Get(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {clientId} не зарегистрирован в системе.");
        }

        var mappedClient = _mapper.Map(client, new ClientResponse());

        return mappedClient;
    }

    public Guid AddClient(ClientRequest client)
    {
        var mappedClient = _mapper.Map<Client>(client);

        _unitOfWork.Clients.Add(mappedClient);
        _unitOfWork.Clients.Save();

        return mappedClient.Id;
    }

    public void DeleteClent(Guid clientId)
    {

    }

    public void UpdateClient(ClientRequest client)
    {

    }
}