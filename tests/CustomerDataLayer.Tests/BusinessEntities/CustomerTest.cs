using CustomerDataLayer.BusinessEntities;
using FluentAssertions;

namespace CustomerDataLayer.Tests.BusinessEntities
{
    public class CustomerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customer = new Customer
            {
                TotalPurchasesAmount = 0
            };

            customer.Should().NotBeNull();

            customer.CustomerId.Should().BeOfType(typeof(int));
            customer.FirstName.Should().BeOfType<string>();
            customer.LastName.Should().BeOfType<string>();
            customer.PhoneNumber.Should().BeOfType<string>();
            customer.Email.Should().BeOfType<string>();
            customer.TotalPurchasesAmount.Should().BeOfType(typeof(decimal));
        }

        [Fact]
        public void ShouldHaveDefaultValues()
        {
            var customer = new Customer();

            customer.CustomerId.Should().Be(0);
            customer.FirstName.Should().Be(string.Empty);
            customer.LastName.Should().Be(string.Empty);
            customer.PhoneNumber.Should().Be(string.Empty);
            customer.Email.Should().Be(string.Empty);
            customer.TotalPurchasesAmount.Should().Be(null);
        }

        [Fact]
        public void ShouldBeAbleToGetSetCustomerId()
        {
            var customer = new Customer
            {
                CustomerId = 1
            };

            customer.CustomerId.Should().Be(1);
        }

        [Fact]
        public void ShouldBeAbleToGetSetFirstName()
        {
            var customer = new Customer()
            {
                FirstName = "First"
            };

            customer.FirstName.Should().Be("First");
        }

        [Fact]
        public void ShouldBeAbleToGetSetLastName()
        {
            var customer = new Customer()
            {
                LastName = "Last"
            };

            customer.LastName.Should().Be("Last");
        }

        [Fact]
        public void ShouldBeAbleToGetSetPhoneNumber()
        {
            var customer = new Customer()
            {
                PhoneNumber = "+12002000000"
            };

            customer.PhoneNumber.Should().Be("+12002000000");
        }

        [Fact]
        public void ShouldBeAbleToGetSetEmail()
        {
            var customer = new Customer()
            {
                Email = "a@b.c"
            };

            customer.Email.Should().Be("a@b.c");
        }

        [Fact]
        public void ShouldBeAbleToGetSetTotalPurchasesAmount()
        {
            var customer = new Customer()
            {
                TotalPurchasesAmount = 10
            };

            customer.TotalPurchasesAmount.Should().Be(10);
        }
    }
}
