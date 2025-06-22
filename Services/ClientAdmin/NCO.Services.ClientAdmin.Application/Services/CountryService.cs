using NCO.Services.ClientAdmin.Application.Interfaces;
using NCO.Services.ClientAdmin.Domain;
using NCO.Services.ClientAdmin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCO.Services.ClientAdmin.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> CreateCountryAsync(CountryDTO dto) => _repository.CreateAsync(dto);
        public Task<bool> UpdateCountryAsync(CountryDTO dto) => _repository.UpdateAsync(dto);
        public Task<CountryDTO> GetCountryByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<List<CountryDTO>> GetAllCountriesAsync() => _repository.GetAllAsync();
        public Task<bool> IsCountryExistsAsync(string name) => _repository.ExistsAsync(name);
        public Task<byte[]> ExportCountriesAsync() => _repository.ExportAsync();
        public async Task<int> GetCountryCountAsync() => await _repository.GetCountAsync();
        public async Task<bool> CheckCountryAsync(string name, string code) => await _repository.CheckAsync(name, code);
        
    }

}
