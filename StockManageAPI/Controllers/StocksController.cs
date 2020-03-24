using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockManageAPI.Data;
using StockManageAPI.Data.Entities;
using Microsoft.Extensions.Logging;

namespace StockManageAPI.Controllers
{
    [Produces("application/json")]
	[ApiController]
    [Route("api/[Controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IStockRepository stockRepository, ILogger<StocksController> logger)
        {
            _stockRepository = stockRepository;
            _logger = logger;
        }

        // GET: api/Stocks
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        public ActionResult<IEnumerable<Stock>> GetStocks()
        {
            try
			{
				return Ok(_stockRepository.GetStockssWithGoodsIn());
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong:" + ex.Message);
				return NotFound();
			}
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var stock = await _stockRepository.ExistAsync(id);

            if (stock == false)
            {
                return NotFound();
            }

            return Ok(await _stockRepository.GetStocksWithGoodsInById(id));
        }

        [HttpPut]
        public async Task<ActionResult<Stock>> PutStock([FromBody]Stock stock)
        {
            try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogError("Invalid model state.");
					return BadRequest();
				}
				else
				{
					stock.DateEdited = DateTime.Now;
					await _stockRepository.UpdateAsync(stock);
					return Ok(stock);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new stock: " + ex.Message);
				return BadRequest();
			}
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock([FromBody]Stock stock)
        {
            try
			{
				if(!ModelState.IsValid)
				{
					_logger.LogError("Invalid model state.");
					return BadRequest();
				}
				else
				{
					stock.DateAdded = DateTime.Now;
					await _stockRepository.CreateAsync(stock);
					return Created($"/api/stock/{stock.Id}", stock);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new good: " + ex.Message);
				return BadRequest();
			}
        }

        // // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stock>> DeleteStock(int id)
        {
            try
			{
				var stock = await _stockRepository.GetByIdAsync(id);
				if (stock != null)
				{
				    await	_stockRepository.DeleteAsync(stock);
					return Ok("Stock deleted");
				}
				return BadRequest("Could not delete stock.");
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception deleting the stock: " + ex.Message);
				return BadRequest();
			}
        }
    }
}
