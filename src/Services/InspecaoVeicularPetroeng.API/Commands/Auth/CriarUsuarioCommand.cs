using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Mediator.Interfaces;

namespace InspecaoVeicularPetroeng.API.Commands.Auth;

public class CriarUsuarioCommand : IRequest<Result>
{
}

public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, Result>
{
    public Task<Result> Handler(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}