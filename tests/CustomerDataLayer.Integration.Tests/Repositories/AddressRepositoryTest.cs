using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;
using FluentAssertions;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class AddressRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            var repository = new AddressRepository();

            repository.Should().NotBeNull();
            repository.TableName.Should().Be("Address");
            repository.UsedByTables.Should().BeEmpty();
            repository.KeyColumn.Should().Be("AddressId");
            repository.NonKeyColumns.Should().Contain(new[] { "CustomerId", "AddressLine", "AddressLine2", "AddressType", "City", "PostalCode", "State", "Country" });
        }

        [Fact]
        public void ShouldBeAbleToGetKeyParameter()
        {
            var repository = new AddressRepository();
            var entity = new Address();

            var keyParameter = repository.GetKeyParameter(entity);

            keyParameter.Should().NotBeNull();

            keyParameter = repository.GetKeyParameter(1);

            keyParameter.Should().NotBeNull();
        }

        [Fact]
        public void ShouldBeAbleToGetNonKeyParameters()
        {
            var repository = new AddressRepository();
            var entity = new Address();

            var nonKeyParameters = repository.GetNonKeyParameters(entity);

            nonKeyParameters.Should().NotBeNull();
            nonKeyParameters.Length.Should().Be(8);
        }
    }
}
