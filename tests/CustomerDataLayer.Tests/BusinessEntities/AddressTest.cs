using CustomerDataLayer.BusinessEntities;
using FluentAssertions;

namespace CustomerDataLayer.Tests.BusinessEntities
{
    public class AddressTest
    {
        [Fact]
        public void ShouldHaveAddressTypes()
        {
            Enum.IsDefined(typeof(AddressType), "Shipping").Should().BeTrue();
            Enum.IsDefined(typeof(AddressType), "Billing").Should().BeTrue();
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var address = new Address();

            address.Should().NotBeNull();

            address.AddressId.Should().BeOfType(typeof(int));
            address.CustomerId.Should().BeOfType(typeof(int));
            address.AddressLine.Should().BeOfType<string>();
            address.AddressLine2.Should().BeOfType<string>();
            address.AddressType.Should().BeOneOf(Enum.GetValues<AddressType>());
            address.City.Should().BeOfType<string>();
            address.PostalCode.Should().BeOfType<string>();
            address.State.Should().BeOfType<string>();
            address.Country.Should().BeOfType<string>();
        }

        [Fact]
        public void ShouldHaveDefaultValues()
        {
            var address = new Address();

            address.AddressId.Should().Be(0);
            address.CustomerId.Should().Be(0);
            address.AddressLine.Should().Be(string.Empty);
            address.AddressLine2.Should().Be(string.Empty);
            address.AddressType.Should().Be(AddressType.Shipping);
            address.City.Should().Be(string.Empty);
            address.PostalCode.Should().Be(string.Empty);
            address.State.Should().Be(string.Empty);
            address.Country.Should().Be(string.Empty);
        }

        [Fact]
        public void ShouldBeAbleToGetSetAddressId()
        {
            var address = new Address
            {
                AddressId = 1
            };

            address.AddressId.Should().Be(1);
        }

        [Fact]
        public void ShouldBeAbleToGetSetCustomerId()
        {
            var address = new Address
            {
                CustomerId = 1
            };

            address.CustomerId.Should().Be(1);
        }

        [Fact]
        public void ShouldBeAbleToGetSetAddressLine()
        {
            var address = new Address
            {
                AddressLine = "Line"
            };

            address.AddressLine.Should().Be("Line");
        }

        [Fact]
        public void ShouldBeAbleToGetSetAddressLine2()
        {
            var address = new Address
            {
                AddressLine2 = "Line2"
            };

            address.AddressLine2.Should().Be("Line2");
        }

        [Fact]
        public void ShouldBeAbleToGetSetAddressType()
        {
            var address = new Address
            {
                AddressType = AddressType.Billing
            };

            address.AddressType.Should().Be(AddressType.Billing);
        }

        [Fact]
        public void ShouldBeAbleToGetSetCity()
        {
            var address = new Address
            {
                City = "City"
            };

            address.City.Should().Be("City");
        }

        [Fact]
        public void ShouldBeAbleToGetSetPostalCode()
        {
            var address = new Address
            {
                PostalCode = "00000"
            };

            address.PostalCode.Should().Be("00000");
        }

        [Fact]
        public void ShouldBeAbleToGetSetState()
        {
            var address = new Address
            {
                State = "State"
            };

            address.State.Should().Be("State");
        }

        [Fact]
        public void ShouldBeAbleToGetSetCountry()
        {
            var address = new Address
            {
                Country = "Country"
            };

            address.Country.Should().Be("Country");
        }
    }
}
