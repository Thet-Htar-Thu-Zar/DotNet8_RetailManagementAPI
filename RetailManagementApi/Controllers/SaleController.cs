using BAL.ISercices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using REPOSITORY.UnitOfWork;
using static MODEL.ApplicationConfig.ResponseModel;

namespace RetailManagementApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleServices _saleServices;

        public SaleController (IUnitOfWork unitOfWork, ISaleServices saleServices)
        {
            _unitOfWork = unitOfWork;
            _saleServices = saleServices;
        }

        [HttpPost("AddSale")]
        public async Task<IActionResult> AddSale(CreateSaleDTO inputModel)
        {
            try
            {
                await _saleServices.AddSale(inputModel);
                return Ok(new ResponseModel { Message = "Sale Create Sucessfully", status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [HttpGet("GetSaleReport")]
        public async Task<IActionResult> GetSaleReport()
        {
            try
            {
                await _saleServices.GetSaleReports();
                return Ok(new ResponseModel { Message = "Sale Reports display successfully", status = APIStatus.Successful });
            }
            catch(Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

    }
}
