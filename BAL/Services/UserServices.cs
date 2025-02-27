﻿using BAL.ISercices;
using BAL.Shared;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;
using System.Text;

namespace BAL.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenProvider _jwtTokenProvider;
        public UserServices(IUnitOfWork unitOfWork, JwtTokenProvider jwtTokenProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task CreateUser(CreateUserDTOs inputModel)
        {
            try
            {
                string hashedPassword = "";
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputModel.Password));
                    hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                var createUser = new Users()
                {
                    UserName = inputModel.UserName,
                    Password = hashedPassword,
                    Amount = inputModel.Amount,
                    UserRole = inputModel.UserRole,
                    CreatedBy = inputModel.CreatedBy,
                };

                await _unitOfWork.User.Add(createUser);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> LoginUser(LoginUserDTOs inputModel)
        {
            try
            {
                string hashedPassword;

                if (string.IsNullOrEmpty(inputModel.UserName) && string.IsNullOrEmpty(inputModel.Password))
                {
                    throw new Exception("UserName and Password must not empty...Pls, fill!");
                }

                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputModel.Password));
                    hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                var checkUser = (await _unitOfWork.User
                    .GetByCondition(x => x.UserName == inputModel.UserName && x.Password == hashedPassword))
                    .FirstOrDefault();

                if (checkUser is null)
                {
                    throw new Exception("UserName and Password is invalid...");
                }

                var role = checkUser.UserRole;

                string token = _jwtTokenProvider.Create(checkUser, role);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user_data = (await _unitOfWork.User.GetByCondition(x => x.UserID == id)).FirstOrDefault();
                if (user_data != null)
                {
                    user_data.ActiveFlag = false;

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            //try
            //{
            //    var lst = await _unitOfWork.User.GetByCondition(x => x.ActiveFlag == true);
            //    var user = _mapper.Map<IEnumerable<CreateUserDTOs>>(lst);
            //    if (lst == null || !lst.Any())
            //    {
            //        throw new Exception("No active user list...");
            //    }
            //    return user;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}

            try
            {
                var lst = (await _unitOfWork.User.GetByCondition(x => x.ActiveFlag == true));

                if (lst == null || !lst.Any())
                {
                    throw new Exception("No active user list...");
                }

                return lst;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Users> GetUserById(Guid id)
        {
            try
            {
                var userlst = (await _unitOfWork.User.GetByCondition(x => x.UserID == id)).FirstOrDefault();

                if (userlst is null)
                {
                    throw new Exception("User doesn't exist....");
                }
                if (userlst.ActiveFlag != true)
                {
                    throw new Exception("User doesn't exist....");

                }

                return userlst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUser(UpdateUserDTOs inputModel)
        {
            try
            {
                var user_data = (await _unitOfWork.User.GetByCondition(x => x.UserID == inputModel.UserID)).FirstOrDefault();

                if (user_data.ActiveFlag != true)
                {
                    throw new Exception("User is not active..");
                }

                if (user_data != null)
                {
                    string updateHashedPassword = "";
                    using (var sha256 = System.Security.Cryptography.SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputModel.Password));
                        updateHashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    }
                    user_data.UserName = inputModel.UserName;
                    user_data.Password = updateHashedPassword;
                    user_data.Amount = inputModel.Amount;
                    user_data.UpdatedBy = "Admin";
                    user_data.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.User.Update(user_data);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
