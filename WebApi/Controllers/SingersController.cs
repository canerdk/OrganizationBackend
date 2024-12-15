using Business.Abstract;
using Entities.Dtos.Singer;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingersController : ControllerBase
    {
        private readonly ISingerService _singerService;

        public SingersController(ISingerService singerService)
        {
            _singerService = singerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _singerService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _singerService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SingerDto dto)
        {
            var result = await _singerService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SingerDto dto)
        {
            var result = await _singerService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _singerService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
