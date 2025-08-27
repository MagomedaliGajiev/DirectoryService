using CSharpFunctionalExtensions;
using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.Domain.Locations;
using Microsoft.Extensions.Logging;
using Shared;

namespace DirectoryService.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<LocationRepository> _logger;

    public LocationRepository(DirectoryServiceDbContext context, ILogger<LocationRepository> logger)
    {
        _dbContext = context;
        _logger = logger;
    }

    public async Task AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Locations.AddAsync(location, cancellationToken);
    }

    public async Task<Result<int, Error>> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var changes = await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success<int, Error>(changes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to insert location");
            return GeneralErrors.Failure("Fail to insert location");
        }
    }
}
