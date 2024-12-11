using AutoMapper;
using GoldBazar.Shared.DTOs;
using Xunit;

namespace Catalog.Test;
public class AutoMapperTests
{
    private readonly IMapper _mapper;

    public AutoMapperTests()
    {
        // Initialize AutoMapper with the MappingProfile
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Catalog.Api.Profiles.MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Catalog.Api.Profiles.MappingProfile>();
        });

        config.AssertConfigurationIsValid(); // Verifies the mapping configuration
    }

    [Fact]
    public void Map_GoldSmith_To_GoldSmithViewModel_WorksCorrectly()
    {
        // Arrange
        var goldsmith = new GoldSmith
        {
            Id = 1,
            Name = "Goldsmith A",
            City = "City A",
            Address = "123 Main St",
            ContactNumber = "123456789",
            Rating = 4.5f
        };

        // Act
        var result = _mapper.Map<GoldSmithViewModel>(goldsmith);

        // Assert
        Assert.Equals(goldsmith.Id, result.Id);
        Assert.Equals(goldsmith.Name, result.Name);
        Assert.Equals(goldsmith.City, result.City);
        Assert.Equals(goldsmith.Address, result.Address);
        Assert.Equals(goldsmith.ContactNumber, result.ContactNumber);
        Assert.Equals(goldsmith.Rating, result.Rating);
    }

    [Fact]
    public void Map_GoldSmithViewModel_To_GoldSmith_WorksCorrectly()
    {
        // Arrange
        var goldsmithViewModel = new GoldSmithViewModel
        {
            Id = 1,
            Name = "Goldsmith A",
            City = "City A",
            Address = "123 Main St",
            ContactNumber = "123456789",
            Rating = 4.5f
        };

        // Act
        var result = _mapper.Map<GoldSmith>(goldsmithViewModel);

        // Assert
        Assert.Equals(goldsmithViewModel.Id, result.Id);
        Assert.Equals(goldsmithViewModel.Name, result.Name);
        Assert.Equals(goldsmithViewModel.City, result.City);
        Assert.Equals(goldsmithViewModel.Address, result.Address);
        Assert.Equals(goldsmithViewModel.ContactNumber, result.ContactNumber);
        Assert.Equals(goldsmithViewModel.Rating, result.Rating);
    }
}
