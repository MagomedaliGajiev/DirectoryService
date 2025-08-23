using DirectoryService.Domain.Locations;

namespace DirectoryService.Application.Locations.Interfaces;

public interface ILocationRepository
{
    Task AddAsync(Location location, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}