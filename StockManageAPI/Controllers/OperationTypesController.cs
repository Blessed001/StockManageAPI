using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockManageAPI.Data;
using StockManageAPI.Data.Entities;

namespace StockManageAPI.Controllers
{
    [Produces("application/json")]
	[ApiController]
    [Route("api/[Controller]")]
    public class OperationTypesController : ControllerBase
    {
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly ILogger<OperationTypesController> _logger;

        public OperationTypesController( IOperationTypeRepository operationTypeRepository, ILogger<OperationTypesController> logger)
        {
            _operationTypeRepository = operationTypeRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All operations</returns>
        [HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
        public ActionResult<IEnumerable<OperationType>> GetOperationTypes()
        {
            try
			{
				return Ok(_operationTypeRepository.GetAll());
			}
			catch (Exception ex)
			{
				_logger.LogError("Something went wrong:" + ex.Message);
				return NotFound();
			}
        }
    }
}
