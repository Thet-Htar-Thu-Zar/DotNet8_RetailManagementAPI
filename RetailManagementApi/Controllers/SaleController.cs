using BAL.ISercices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL.ApplicationConfig;
using MODEL.DTOs;
using MODEL.Entities;
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
        [HttpPost("AddMultipleSale")]
        public async Task<IActionResult> AddSaleMultiple(IEnumerable<CreateSaleDTO> inputModel)
        {
            try
            {
                await _saleServices.AddSaleMultiple(inputModel);
                return Ok(new ResponseModel { Message = "Sale Create Sucessfully", status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }

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
                var salereport = await _saleServices.GetSaleReports();
                var activeSalereport = salereport.Where(p => p.ActiveFlag == true).ToList();

                return Ok(new ResponseModel { Message = "Sale Reports display successfully", status = APIStatus.Successful, Data = activeSalereport });
            }
            catch(Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

        [HttpGet("GetSaleReportById")]
        public async Task<IActionResult> GetSaleReportById(Guid id)
        {
            try
            {
                var salereport = await _saleServices.GetSaleReportById(id);
                
                return Ok(new ResponseModel { Message = "Sale Reports display successfully", status = APIStatus.Successful, Data = salereport });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [HttpGet("GetSaleSummary")]
        public async Task<IActionResult> GetSaleSummary()
        {
            try
            {
                var salereport = await _saleServices.GetAllSaleSummary();
                return Ok(new ResponseModel { Message = "Sale Reports display successfully", status = APIStatus.Successful, Data = salereport });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

        [HttpDelete("DeleteSaleReport")]
        public async Task<IActionResult> DeleteSale(DeleteSale inputModel)
        {
            try
            {
                await _saleServices.DeleteSale(inputModel);
                return Ok(new ResponseModel { Message = "Delete Sucessfully", status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }
    }
}
