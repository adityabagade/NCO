using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NCO.Services.ClientAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;   

namespace NCO.Services.ClientAdmin.Infrastructure
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _connectionString;

        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerDb");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<bool> CreateAsync(CountryDTO dto)
        {
            var sql = "INSERT INTO Countries (CountryName, CountryCode) VALUES (@Name, @Code)";
            using var con = Connection;
            var result = await con.ExecuteAsync(sql, new { Name = dto.CountryName, Code = dto.CountryCode });
            return result > 0;
        }

        public async Task<bool> UpdateAsync(CountryDTO dto)
        {
            var sql = "UPDATE Countries SET CountryName = @Name, CountryCode = @Code WHERE CountryId = @Id";
            using var con = Connection;
            var result = await con.ExecuteAsync(sql, new { Name = dto.CountryName, Code = dto.CountryCode, Id = dto.CountryId });
            return result > 0;
        }

        public async Task<CountryDTO> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Countries WHERE CountryId = @Id";
            using var con = Connection;
            return await con.QueryFirstOrDefaultAsync<CountryDTO>(sql, new { Id = id });
        }

        public async Task<List<CountryDTO>> GetAllAsync()
        {
            var sql = "SELECT * FROM Countries";
            using var con = Connection;
            var result = await con.QueryAsync<CountryDTO>(sql);
            return result.ToList();
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var sql = "SELECT COUNT(*) FROM Countries WHERE LOWER(CountryName) = LOWER(@Name)";
            using var con = Connection;
            var count = await con.ExecuteScalarAsync<int>(sql, new { Name = name });
            return count > 0;
        }

        public async Task<byte[]> ExportAsync()
        {
            var countries = await GetAllAsync();
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.WriteLine("CountryId,CountryName,CountryCode");

            foreach (var c in countries)
                writer.WriteLine($"{c.CountryId},{c.CountryName},{c.CountryCode}");

            writer.Flush();
            return ms.ToArray();
        }

        public async Task<int> GetCountAsync()
        {
            var sql = "SELECT COUNT(*) FROM Countries";
            using var con = new SqlConnection(_connectionString);
            return await con.ExecuteScalarAsync<int>(sql);
        }

        public async Task<bool> CheckAsync(string name, string code)
        {
            var sql = @"SELECT COUNT(*) FROM Countries 
                WHERE (@Name IS NULL OR LOWER(CountryName) = LOWER(@Name))
                AND (@Code IS NULL OR LOWER(CountryCode) = LOWER(@Code))";

            using var con = new SqlConnection(_connectionString);
            var count = await con.ExecuteScalarAsync<int>(sql, new { Name = name, Code = code });
            return count > 0;
        }
    }
}
