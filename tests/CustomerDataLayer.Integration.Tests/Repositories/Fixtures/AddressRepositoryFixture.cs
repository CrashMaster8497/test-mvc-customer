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

        public static int? Create(Address address, Customer customer)
        {
            address.CustomerId = customer.Id;

            var repository = new AddressRepository();

            int? id = repository.Create(address);
            address.Id = id ?? address.Id;

            return id;
        }

        public static int? Create(Address address)
        {
            var customer = CustomerRepositoryFixture.GetMinCustomer();
            CustomerRepositoryFixture.Create(customer);

            return Create(address, customer);
        }

        public static List<Address> ReadByCustomerId(int customerId)
        {
            var repository = new AddressRepository();
            return repository.ReadByCustomerId(customerId);
        }

        public static int DeleteByCustomerId(int customerId)
        {
            var repository = new AddressRepository();
            return repository.DeleteByCustomerId(customerId);
        }
    }
}
