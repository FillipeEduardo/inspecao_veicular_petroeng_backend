using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using InspecaoVeicularPetroeng.API.Helpers;
using InspecaoVeicularPetroeng.Domain.Results;
using InspecaoVeicularPetroeng.Infrastructure.Data;
using InspecaoVeicularPetroeng.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InspecaoVeicularPetroeng.API.Commands.Foto;

public class CriarFotoCommand : IRequest<Result>
{
    public IFormFile Foto { get; set; } = null!;
    public long VistoriaId { get; set; }
    public int EvidenciaId { get; set; }

    public static implicit operator Domain.Entities.Foto(CriarFotoCommand command)
    {
        return new Domain.Entities.Foto
        {
            EvidenciaId = command.EvidenciaId,
            Extensao = Path.GetExtension(command.Foto.FileName),
            Id = Guid.NewGuid(),
            VistoriaId = command.VistoriaId
        };
    }
}

public class CriarFotoCommandHandler(AppDbContext context, IAmazonS3 amazon) : IRequestHandler<CriarFotoCommand, Result>
{
    public async Task<Result> Handler(CriarFotoCommand request, CancellationToken cancellationToken)
    {
        var existeEssaEvidencia =
            await context.Evidencias.AnyAsync(e => e.Id == request.EvidenciaId, cancellationToken);
        if (!existeEssaEvidencia)
            return new ErrorResult(["A evidência não foi cadastrada."], HttpStatusCode.BadGateway);

        var existeEssaVistoria = await context.Vistorias.AnyAsync(v => v.Id == request.VistoriaId, cancellationToken);
        if (!existeEssaVistoria) return new ErrorResult(["A vistoria não foi cadastrada."], HttpStatusCode.BadGateway);

        var fotoExistente = await context.Fotos.AnyAsync(
            f => f.EvidenciaId == request.EvidenciaId && f.VistoriaId == request.VistoriaId, cancellationToken);
        if (fotoExistente)
            return new ErrorResult(["Já foi cadastrado uma foto para essa evidência."], HttpStatusCode.BadGateway);

        Domain.Entities.Foto foto = request;
        await context.AddAsync(foto, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await using var stream = request.Foto.OpenReadStream();

        await amazon.PutObjectAsync(new PutObjectRequest
        {
            Key = $"{foto.Id}{foto.Extensao}",
            BucketName = BucketNames.FotosEvidencias,
            InputStream = stream,
            ContentType = request.Foto.ContentType
        }, cancellationToken);

        return new SuccessResult("Foto Criada com sucesso.", HttpStatusCode.OK);
    }
}