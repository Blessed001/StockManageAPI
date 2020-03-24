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
    public class GoodInStocksController : ControllerBase
    {
        private readonly IGoodInStockRepository _goodInStockRepository;
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly ILogger<GoodInStocksController> _logger;

        public GoodInStocksController(IGoodInStockRepository goodInStockRepository,
                                     IOperationTypeRepository operationTypeRepository,
                                     ILogger<GoodInStocksController> logger)
        {
            _goodInStockRepository = goodInStockRepository;
            _operationTypeRepository = operationTypeRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All GoodInStocks</returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        public ActionResult<IEnumerable<GoodInStock>> GetGoodInStocks()
        {
            try
			{
				return Ok(_goodInStockRepository.GetAll());
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
        /// <returns>GoodInStock by Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GoodInStock>> GetGoodInStock(int id)
        {
            var goodInStock = await _goodInStockRepository.ExistAsync(id);

            if (goodInStock == false)
            {
                return NotFound();
            }

            return Ok(await _goodInStockRepository.GetByIdAsync(id));
        }

        /// <summary>
        /// for test use idOperation {"Внутреннее перемещение": 1, "Расход": 2,"Приход": 3} -> check OperationTipe model
        /// </summary>
        /// <param name="idOperation"></param>
        /// <param name="goodInStock"></param>
        /// <returns>Create operations</returns>
        [HttpPost("{idOperation}")]
        public async Task<ActionResult<GoodInStock>> DoOPeration([FromRoute]int idOperation,[FromBody]GoodInStock goodInStock)
        {
            var operation = await _operationTypeRepository.GetByIdAsync(idOperation);
            bool valid = _goodInStockRepository.ValidateGood(goodInStock);
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid model state.");
                    return BadRequest();
                }
                if(valid == true && (operation.Name == "Расход" || operation.Name == "Приход" || operation.Name == "Внутреннее перемещение"))
                {
                    return BadRequest();
                }
                else
                {
                    await _goodInStockRepository.DoOperation(idOperation, goodInStock);
                    return Created($"/api/goodInStock/{goodInStock.Id}", goodInStock);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception adding new goodInStock: " + ex.Message);
                return BadRequest();
            }
        }
    }
}
