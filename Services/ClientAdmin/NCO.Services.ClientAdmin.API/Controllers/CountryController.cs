using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NCO.Services.ClientAdmin.Application.Interfaces;
using NCO.Services.ClientAdmin.Domain;

namespace NCO.Services.ClientAdmin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CountryDTO dto)
        {
            var result = await _countryService.CreateCountryAsync(dto);
            return Ok(result ? "Country created successfully." : "Creation failed.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CountryDTO dto)
        {
            var result = await _countryService.UpdateCountryAsync(dto);
            return Ok(result ? "Country updated successfully." : "Update failed.");
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            return country != null ? Ok(country) : NotFound();
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("exists")]
        public async Task<IActionResult> Exists([FromQuery] string name)
        {
            var exists = await _countryService.IsCountryExistsAsync(name);
            return Ok(new { exists });
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var fileBytes = await _countryService.ExportCountriesAsync();
            return File(fileBytes, "application/octet-stream", "Countries.csv");
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _countryService.GetCountryCountAsync();
            return Ok(new { total = count });
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check([FromQuery] string name, [FromQuery] string code)
        {
            var exists = await _countryService.CheckCountryAsync(name, code);
            return Ok(new { exists });
        }
    }
}
