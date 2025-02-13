using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
        public Task<Stock?> GetByIdAsync(int id);
        public Task<Stock> CreateAsync(CreateStockDto stock);
        public Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);
        public Task<Stock?> DeleteAsync(int id);

    }
}