using CSharpFunctionalExtensions;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.Shared;

namespace DirectoryService.Application.Locations.Interfaces;

public interface ILocationRepository
{
    Task AddAsync(Location location, CancellationToken cancellationToken = default);

    Task<Result<int, Error>> SaveChangesAsync(CancellationToken cancellationToken);
}