using Business.Abstract;
using Entities.Dtos.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractsController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contractService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _contractService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContractDto dto)
        {
            var result = await _contractService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContractDto dto)
        {
            var result = await _contractService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contractService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
