using CustomerDataLayer.BusinessEntities;
using CustomerDataLayer.Repositories;

namespace CustomerDataLayer.Integration.Tests.Repositories
{
    public class CustomerRepositoryFixture
    {
        public static CustomerRepository GetRepository()
        {
            return new CustomerRepository();
        }

        public static Customer GetMinCustomer()
        {
            return new Customer
            {
                LastName = "Last"
            };
        }

        public static Customer GetMaxCustomer()
        {
            return new Customer
            {
                FirstName = "First",
                LastName = "Last",
                PhoneNumber = "+12002000000",
                Email = "a@a.a",
                TotalPurchasesAmount = 0
            };
        }

        public static void ModifyCustomer(Customer customer)
        {
            customer.FirstName = "New First";
            customer.LastName = "New Last";
        }

        public static int? Create(Customer customer)
        {
            var repository = new CustomerRepository();
            return repository.Create(customer);
        }
    }
}
