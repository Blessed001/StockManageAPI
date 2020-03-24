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
    public class GoodsController : ControllerBase
    {
        private readonly IGoodRepository _goodRepository;
        private readonly ILogger<GoodsController> _logger;

        public GoodsController(IGoodRepository goodRepository, ILogger<GoodsController> logger)
        {
            _goodRepository = goodRepository;
            _logger = logger;
        }

        // GET: api/Goods
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        public ActionResult<IEnumerable<Good>> GetGoods()
        {
            try
			{
				return Ok(_goodRepository.GetGoodsWithInventories());
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong:" + ex.Message);
				return NotFound();
			}
        }

        // GET: api/Goods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Good>> GetGood(int id)
        {
            var good = await _goodRepository.ExistAsync(id);

            if (good == false)
            {
                return NotFound();
            }

            return Ok(await _goodRepository.GetGoodsWithInventoriesById(id));
        }

        [HttpPut]
        public async Task<ActionResult<Good>> PutGood([FromBody]Good good)
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
					await _goodRepository.UpdateAsync(good);
					return Ok(good);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new good: " + ex.Message);
				return BadRequest();
			}
        }

        [HttpPost]
        public async Task<ActionResult<Good>> PostGood([FromBody]Good good)
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
					await _goodRepository.CreateAsync(good);
					return Created($"/api/good/{good.Id}", good);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception adding new good: " + ex.Message);
				return BadRequest();
			}
        }

        // // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Good>> DeleteGood(int id)
        {
            try
			{
				var good = await _goodRepository.GetByIdAsync(id);
				if (good != null)
				{
				    await	_goodRepository.DeleteAsync(good);
					return Ok("Good deleted");
				}
				return BadRequest("Could not delete good.");
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception deleting the good: " + ex.Message);
				return BadRequest();
			}
        }
    }
}
