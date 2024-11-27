using Business.Abstract;
using Business.Concrete;
using Entities.Dtos.Contract;
using Entities.Dtos.Organization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _organizationService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _organizationService.GetById(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrganizationDto dto)
        {
            var result = await _organizationService.Add(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrganizationDto dto)
        {
            var result = await _organizationService.Update(dto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _organizationService.Delete(id);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
