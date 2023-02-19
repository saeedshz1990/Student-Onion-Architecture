using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers.Courses;

[ApiController]
[Route("api/courses")]
public class CourseControllers : ControllerBase
{
    private readonly ICourseApplication _service;

    public CourseControllers(ICourseApplication service)
    {
        _service = service;
    }

    [HttpPost]
    public void Add(AddCourseDto dto)
    {
        _service.Add(dto);
    }

    [HttpPut("{id}")]
    public void Update(int id, UpdateCourseDto dto)
    {
        _service.Update(dto, id);
    }

    [HttpGet]
    public IList<GetCourseDto> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public GetCourseByIdDto GetById(int id)
    {
        return _service.GetById(id);
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _service.Delete(id);
    }
}