using Business.Abstract;
using Entities.Dtos.Participant;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _participantService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _participantService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ParticipantDto dto)
        {
            var result = await _participantService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ParticipantDto dto)
        {
            var result = await _participantService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _participantService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
