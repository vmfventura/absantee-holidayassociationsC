namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Domain.IRepository;
using Gateway;
using RabbitMQ.Client.Events;

public class AssociationsService
{
    private readonly IAssociationsRepository _associationsRepository;

    public AssociationsService(IAssociationsRepository associationsRepository)
    {
        _associationsRepository = associationsRepository;
    }
}