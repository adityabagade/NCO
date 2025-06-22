using NCO.Services.ClientAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCO.Services.ClientAdmin.Application.Interfaces
{
    public interface ICountryService
    {
        Task<bool> CreateCountryAsync(CountryDTO dto);
        Task<bool> UpdateCountryAsync(CountryDTO dto);
        Task<CountryDTO> GetCountryByIdAsync(int id);
        Task<List<CountryDTO>> GetAllCountriesAsync();
        Task<bool> IsCountryExistsAsync(string countryName);
        Task<byte[]> ExportCountriesAsync();
        Task<bool> CheckCountryAsync(string name, string code);

        Task<int> GetCountryCountAsync();
    }
}
