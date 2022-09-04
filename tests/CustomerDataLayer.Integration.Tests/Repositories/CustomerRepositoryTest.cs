using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;
using FluentAssertions;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerRepository()
        {
            var repository = new CustomerRepository();

            repository.Should().NotBeNull();
            repository.TableName.Should().Be("Customer");
            //repository.UsedByTables.Should().Contain("Address");
            //repository.UsedByTables.Should().Contain("Note");
            repository.KeyColumn.Should().Be("CustomerId");
            repository.NonKeyColumns.Should().Contain(new[] { "FirstName", "LastName", "PhoneNumber", "Email", "TotalPurchasesAmount" });
        }

        [Fact]
        public void ShouldBeAbleToGetKeyParameter()
        {
            var repository = new CustomerRepository();
            var entity = new Customer();

            var keyParameter = repository.GetKeyParameter(entity);

            keyParameter.Should().NotBeNull();

            keyParameter = repository.GetKeyParameter(1);

            keyParameter.Should().NotBeNull();
        }

        [Fact]
        public void ShouldBeAbleToGetNonKeyParameters()
        {
            var repository = new CustomerRepository();
            var entity = new Customer();

            var nonKeyParameters = repository.GetNonKeyParameters(entity);

            nonKeyParameters.Should().NotBeNull();
            nonKeyParameters.Length.Should().Be(5);
        }
    }
}
