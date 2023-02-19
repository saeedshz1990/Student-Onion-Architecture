namespace StudentManagement.Application.Contracts.Dto
{
    public class AddStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public int YearBirth { get; set; }
        public int TeacherId { get; set; }
    }
}
