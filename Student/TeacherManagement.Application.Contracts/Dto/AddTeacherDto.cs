namespace TeacherManagement.Application.Contracts.Dto
{
    public class AddTeacherDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public int YearBirth { get; set; }
    }
}
