using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(CreateStockDto stock)
        {
            var stockModel = stock.ToStockModel();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;

        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var existingModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if(existingModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(existingModel);
    
            await _context.SaveChangesAsync();

            return existingModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var existingModel = await _context.Stocks.FindAsync(id);
            return existingModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto)
        {
            var existingModel = await _context.Stocks.FindAsync(id);

            if(existingModel == null)
            {
                return null;
            }
            var newStockModel = stockDto.ToStockModel();
             _context.Remove(existingModel);
             await _context.AddAsync(newStockModel);
             await _context.SaveChangesAsync();

             return newStockModel;

        }
    }
}