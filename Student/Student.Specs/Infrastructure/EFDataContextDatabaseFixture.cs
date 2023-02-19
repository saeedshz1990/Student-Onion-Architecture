using CourseManagement.Infrastructure.EFCore;
using Xunit;

namespace Student.Specs.Infrastructure;

[Collection(nameof(ConfigurationFixture))]
public class EFDataContextDatabaseFixture : DatabaseFixture
{
    readonly ConfigurationFixture _configuration;

    public EFDataContextDatabaseFixture(ConfigurationFixture configuration)
    {
        _configuration = configuration;
    }

    public EFDataContext CreateDataContext()
    {
        return new EFDataContext(_configuration.Value.DbConnectionString);
    }

    public CourseManagementContext CreateDataContext1()
    {
        return new CourseManagementContext(_configuration.Value.DbConnectionString);
    }
}