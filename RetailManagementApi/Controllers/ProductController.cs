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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductServices _productService;

        public ProductController(IUnitOfWork unitOfWork, IProductServices productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        
        public async Task <IActionResult> GetAllProduct() 
        {
            try
            {
                var productdata = await _unitOfWork.Product.GetAll();
                return Ok(new ResponseModel { Message = "Successfully", status = APIStatus.Successful, Data = productdata});
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [HttpPost("AddProduct")]

        public async Task<IActionResult> AddProduct (AddProductDTO inputModel)
        {
            try
            {
                await _productService.AddProduct(inputModel);
                return Ok(new ResponseModel { Message = "Add Sucessfully", status = APIStatus.Successful });
            }
            catch(Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

        [HttpPost("UpdateProduct")]

        public async Task<IActionResult> UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                await _productService.UpdateProduct(inputModel);
                return Ok(new ResponseModel { Message = "Update Sucessfully", status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }
    }
}
