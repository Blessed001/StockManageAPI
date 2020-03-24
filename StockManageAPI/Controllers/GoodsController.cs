using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockManageAPI.Data;
using StockManageAPI.Data.Entities;

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

		/// <summary>
		/// 
		/// </summary>
		/// <returns>All Goods</returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        public ActionResult<IEnumerable<Good>> GetGoods()
        {
            try
			{
				return Ok(_goodRepository.GetGoodsWithGoodsIn());
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong:" + ex.Message);
				return NotFound();
			}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Good by Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Good>> GetGood(int id)
        {
            var good = await _goodRepository.ExistAsync(id);

            if (good == false)
            {
                return NotFound();
            }

            return Ok(await _goodRepository.GetGoodsWithGoodsInById(id));
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="good"></param>
		/// <returns>Update good</returns>
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
					good.DateEdited = DateTime.Now;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="good"></param>
        /// <returns>Add good</returns>
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
					good.DateAdded = DateTime.Now;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>delete good</returns>
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
