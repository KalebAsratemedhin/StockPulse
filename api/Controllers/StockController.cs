using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly IStockRepository  _stockRepo;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepo = stockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocksModel = await _stockRepo.GetAllAsync();

            var stocksDto = stocksModel.Select(stock => stock.ToStockDto());

            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            var result = await _stockRepo.GetByIdAsync(id);


            if(result == null)
            {
                return NotFound();
            }
            return Ok(result.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {   
            var result = await _stockRepo.CreateAsync(stockDto);

            return CreatedAtAction(nameof(GetOne), new { id = result.Id}, result.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto stockDto)
        {   
            var result = await _stockRepo.UpdateAsync(id, stockDto);
            
            if(result == null)
            {
                return NotFound();
            }
           
            return Ok(result.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {   
            var result = await _stockRepo.DeleteAsync(id);
            if(result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}