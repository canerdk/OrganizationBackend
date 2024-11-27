using Business.Abstract;
using Entities.Dtos.Event;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _eventService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _eventService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EventDto dto)
        {
            var result = await _eventService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EventDto dto)
        {
            var result = await _eventService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _eventService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
