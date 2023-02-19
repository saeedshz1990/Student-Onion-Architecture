using Microsoft.AspNetCore.Mvc;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Application.Contracts.Teacher;

namespace ServiceHost.Controllers.Teachers;

[ApiController]
[Route("api/teachers")]
public class TeacherControllers : ControllerBase
{
    private readonly ITeacherApplication _service;

    public TeacherControllers(ITeacherApplication service)
    {
        _service = service;
    }

    [HttpPost]
    public void Add(AddTeacherDto dto)
    {
        _service.Add(dto);
    }

    [HttpGet]
    public IList<GetTeacherDto> GetAll()
    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public GetTeacherByIdDto GetById(int id)
    {
        return _service.GetById(id);
    } 

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        _service.Delete(id);
    }

    [HttpPut("{id}")]
    public void Update(UpdateTeacherDto dto, int id)
    {
        _service.Update(dto, id);
    }
}