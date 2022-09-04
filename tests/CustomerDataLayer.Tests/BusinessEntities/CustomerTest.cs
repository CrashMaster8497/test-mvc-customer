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
                Id = 0,
                FirstName = string.Empty,
                LastName = string.Empty,
                PhoneNumber = string.Empty,
                Email = string.Empty,
                TotalPurchasesAmount = 0
            };

            customer.Should().NotBeNull();

            customer.Id.Should().BeOfType(typeof(int));
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

            customer.Id.Should().Be(0);
            customer.FirstName.Should().Be(null);
            customer.LastName.Should().Be(string.Empty);
            customer.PhoneNumber.Should().Be(null);
            customer.Email.Should().Be(null);
            customer.TotalPurchasesAmount.Should().Be(null);
        }

        [Fact]
        public void ShouldBeAbleToGetSetCustomerId()
        {
            var customer = new Customer
            {
                Id = 1
            };

            customer.Id.Should().Be(1);
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
