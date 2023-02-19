using ChooseCourseManagement.Application.Contracts.ChooseCourse;
using ChooseCourseManagement.Application.Contracts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers.ChooseCourse
{
    [ApiController]
    [Route("api/[chooseCourses]")]
    public class ChooseCourseController : ControllerBase
    {
        private readonly IChooseCourseApplication _service;

        public ChooseCourseController(IChooseCourseApplication service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddChooseCourseDto dto)
        {
            _service.Add(dto);
        }
    }
}
