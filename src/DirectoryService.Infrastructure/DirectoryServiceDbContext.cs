﻿using DirectoryService.Domain.DepartmentLocations;
using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace DirectoryService.Infrastructure;

public class DirectoryServiceDbContext : DbContext
{
    private readonly string _connectionString;

    public DirectoryServiceDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Department> Departments { get; set; }

    public DbSet<Location> Locations { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<DepartmentLocation> DepartmentLocations { get; set; }

    public DbSet<DepartmentPosition> DepartmentPositions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);

        optionsBuilder.EnableDetailedErrors();

        optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(build => build.AddConsole()));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("ltree");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DirectoryServiceDbContext).Assembly);
    }
}
