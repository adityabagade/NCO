using NCO.Services.ClientAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCO.Services.ClientAdmin.Infrastructure
{
    public interface ICountryRepository
    {
        Task<bool> CreateAsync(CountryDTO dto);
        Task<bool> UpdateAsync(CountryDTO dto);
        Task<CountryDTO> GetByIdAsync(int id);
        Task<List<CountryDTO>> GetAllAsync();
        Task<bool> ExistsAsync(string name);
        Task<byte[]> ExportAsync();
        Task<bool> CheckAsync(string name, string code);

        Task<int> GetCountAsync();
    }
}
