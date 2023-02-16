namespace _0_Framework.Domain
{
    public class PersonEntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public int YearBirth { get; set; }
        public DateTime CreationDate { get; set; }
    }
}