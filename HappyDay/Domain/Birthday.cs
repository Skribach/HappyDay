namespace HappyDay.Domain
{
    public class Birthday
    {
        public string Id { get; set; }
        public string? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BiDay { get; set; }
    }
}
