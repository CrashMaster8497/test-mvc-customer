using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;

namespace CustomerDataLayer.Integration.Tests.Repositories.Fixtures
{
    public class AddressRepositoryFixture
    {
        public static AddressRepository GetRepository()
        {
            return new AddressRepository();
        }

        public static Address GetMinAddress()
        {
            return new Address
            {
                AddressLine = "Line",
                AddressType = AddressType.Shipping,
                City = "City",
                PostalCode = "00000",
                State = "State",
                Country = "United States"
            };
        }

        public static Address GetMaxAddress()
        {
            return new Address
            {
                AddressLine = "Line",
                AddressLine2 = "Line2",
                AddressType = AddressType.Shipping,
                City = "City",
                PostalCode = "00000",
                State = "State",
                Country = "United States"
            };
        }

        public static void ModifyAddress(Address address)
        {
            address.AddressLine = "New Line";
            address.AddressLine2 = "New Line2";
        }

        public static int? Create(Address address)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = CustomerRepositoryFixture.Create(customer)!.Value;
            address.CustomerId = customer.Id;

            var repository = new AddressRepository();
            return repository.Create(address);
        }

        public static List<int> Create(List<Address> addresses)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            customer.Id = CustomerRepositoryFixture.Create(customer)!.Value;
            foreach (var address in addresses)
            {
                address.CustomerId = customer.Id;
            }

            var ids = new List<int>();
            ids.AddRange(addresses.Select(address => Create(address)!.Value));
            return ids;
        }
    }
}
