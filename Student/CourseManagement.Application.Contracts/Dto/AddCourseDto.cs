namespace CourseManagement.Application.Contracts.Dto
{
    public class AddCourseDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
