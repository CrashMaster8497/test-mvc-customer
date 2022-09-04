using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Integration.Tests.Repositories.Fixtures;
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

        [Theory]
        [MemberData(nameof(GenerateAddressGroups))]
        public void ShouldBeAbleToReadByCustomerId(List<List<Address>> addressesList)
        {
            var customers = new List<Customer>();
            foreach (var addresses in addressesList)
            {
                var customer = CustomerRepositoryFixture.GetMinCustomer();
                CustomerRepositoryFixture.Create(customer);
                customers.Add(customer);

                foreach (var address in addresses)
                {
                    AddressRepositoryFixture.Create(address, customer);
                }
            }

            for (int i = 0; i < addressesList.Count; i++)
            {
                var readAddresses = AddressRepositoryFixture.ReadByCustomerId(customers[i].Id);

                readAddresses.Should().BeEquivalentTo(addressesList[i]);
            }
        }

        [Theory]
        [MemberData(nameof(GenerateAddressGroups))]
        public void ShouldBeAbleToDeleteByCustomerId(List<List<Address>> addressesList)
        {
            var customers = new List<Customer>();
            foreach (var addresses in addressesList)
            {
                var customer = CustomerRepositoryFixture.GetMinCustomer();
                CustomerRepositoryFixture.Create(customer);
                customers.Add(customer);

                foreach (var address in addresses)
                {
                    AddressRepositoryFixture.Create(address, customer);
                }
            }

            for (int i = 0; i < addressesList.Count; i++)
            {
                int affectedRows = AddressRepositoryFixture.DeleteByCustomerId(customers[i].Id);
                var deletedAddresses = AddressRepositoryFixture.ReadByCustomerId(customers[i].Id);

                affectedRows.Should().Be(addressesList[i].Count);
                deletedAddresses.Should().BeEmpty();

                for (int j = i + 1; j < addressesList.Count; j++)
                {
                    var notDeletedAddresses = AddressRepositoryFixture.ReadByCustomerId(customers[j].Id);

                    notDeletedAddresses.Should().BeEquivalentTo(addressesList[j]);
                }
            }
        }

        private static IEnumerable<object[]> GenerateAddressGroups()
        {
            yield return new object[] { new List<List<Address>>
            {
                Enumerable.Range(0, 1).Select(_ => AddressRepositoryFixture.GetMinAddress()).ToList(),
                Enumerable.Range(0, 0).Select(_ => AddressRepositoryFixture.GetMinAddress()).ToList(),
                Enumerable.Range(0, 3).Select(_ => AddressRepositoryFixture.GetMinAddress()).ToList()
            } };
        }
    }
}
