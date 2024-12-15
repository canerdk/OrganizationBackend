using Business.Abstract;
using Business.Concrete;
using Entities.Dtos.Participant;
using Entities.Dtos.UserContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContractsController : ControllerBase
    {
        private readonly IUserContractService _userContractService;

        public UserContractsController(IUserContractService userContractService)
        {
            _userContractService = userContractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userContractService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userContractService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpGet("GetBySessionId/{guid}")]
        public async Task<IActionResult> GetBySessionId(Guid guid)
        {
            var result = await _userContractService.GetBySessionId(guid);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserContractDto dto)
        {
            var result = await _userContractService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserContractDto dto)
        {
            var result = await _userContractService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userContractService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
