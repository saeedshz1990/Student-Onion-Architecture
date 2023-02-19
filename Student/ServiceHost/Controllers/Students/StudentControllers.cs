using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Application.Contracts.Student;

namespace ServiceHost.Controllers.Students;

[ApiController]
[Route("api/students")]
public class StudentControllers : ControllerBase
{
    private readonly IStudentApplication _service;

    public StudentControllers(IStudentApplication service)
    {
        _service = service;
    }

    [HttpPost]
    public void Add(AddStudentDto dto)
    {
         _service.Add(dto);
    }

    [HttpPut("{id}")]
    public void Update(UpdateStudentDto dto, int id)
    {
         _service.Update(dto, id);
    }

    [HttpGet]
    public IList<GetStudentDto> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public GetStudentByIdDto GetById(int id)
    {
        return _service.GetById(id);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _service.Delete(id);
    }
}