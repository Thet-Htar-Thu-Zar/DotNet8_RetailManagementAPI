using BAL.ISercices;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserServices _userServices;

        public UserController(IUnitOfWork unitOfWork, IUserServices userServices)
        {
            _unitOfWork = unitOfWork;
            _userServices = userServices;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserDTOs inputModel)
        {
            try
            {
                await _userServices.CreateUser(inputModel);
                return Ok(new ResponseModel { Message = "Add Sucessfully", status = APIStatus.Successful });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserDTOs inputModel)
        {
            try
            {
                var data = await _userServices.LoginUser(inputModel);
                return Ok(new ResponseModel { Message = "Login Sucessfully", status = APIStatus.Successful, Data = data });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var userList = await _userServices.GetAllUsers();

                return Ok(new ResponseModel { Message = "Display Sucessfully", status = APIStatus.Successful, Data = userList });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var users = await _userServices.GetUserById(id);

                return Ok(new ResponseModel { Message = "User Record display successfully", status = APIStatus.Successful, Data = users });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTOs inputModel)
        {
            try
            {
                await _userServices.UpdateUser(inputModel);
                return Ok(new ResponseModel { Message = "Update Sucessfully", status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userServices.DeleteUser(id);
                return Ok(new ResponseModel { Message = "Delete Sucessfully", status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, status = APIStatus.Error });

            }
        }

    }
}
