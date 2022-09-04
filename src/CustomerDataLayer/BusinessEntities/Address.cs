namespace CustomerDataLayer.BusinessEntities
{
    public class Address : Entity
    {
        public override int Id { get; set; } = 0;
        public int CustomerId { get; set; } = 0;
        public string AddressLine { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = null;
        public AddressType AddressType { get; set; } = AddressType.Shipping;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public enum AddressType
    {
        Shipping,
        Billing
    }
}
