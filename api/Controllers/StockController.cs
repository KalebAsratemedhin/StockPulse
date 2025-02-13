using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocksModel = await _context.Stocks.ToListAsync();

            var stocksDto = stocksModel.Select(stock => stock.ToStockDto());

            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {   
            var stockModel = stockDto.ToStockModel();
            await _context.Stocks.AddAsync(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOne), new { id = stockModel.Id}, stockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {   
            var existingModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if(existingModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(existingModel);
            var stockModel = stockDto.ToStockModel();
            await _context.Stocks.AddAsync(stockModel);
            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   
            var existingModel = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if(existingModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(existingModel);
    
            _context.SaveChanges();

            return NoContent();
        }

    }
}