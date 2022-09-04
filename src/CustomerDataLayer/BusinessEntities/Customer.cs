namespace CustomerDataLayer.BusinessEntities
{
    public class Customer : Entity
    {
        public override int Id { get; set; } = 0;
        public string? FirstName { get; set; } = null;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = null;
        public string? Email { get; set; } = null;
        public decimal? TotalPurchasesAmount { get; set; } = null;
    }
}
