namespace CustomerDataLayer.BusinessEntities
{
    public class Note : Entity
    {
        public override int Id { get; set; } = 0;
        public int CustomerId { get; set; } = 0;
        public string Text { get; set; } = string.Empty;
    }
}
