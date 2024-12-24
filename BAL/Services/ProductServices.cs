using BAL.ISercices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;

namespace BAL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public async Task<Products> GetProductById (GetProductByIdDTO inputModel)
        //{
        //    try
        //    {
        //        Guid pid = inputModel.ProductID;
        //        var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == pid)).FirstOrDefault();
        //        if(product_data != null)
        //        {
        //            await _unitOfWork.Product.GetById(pid);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task AddProduct(AddProductDTO inputModel)
        {
            try
            {
                var addproduct = new Products()
                {
                    ProductName = inputModel.ProductName,
                    RemainingStock = Convert.ToInt32(inputModel.RemainingStock),
                    ProductPrice = Convert.ToDecimal(inputModel.ProductPrice),
                    ProductProfit = Convert.ToDecimal(inputModel.ProductProfit),
                    CreatedBy= inputModel.CreatedBy,
                };
                await _unitOfWork.Product.Add(addproduct);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateProduct (UpdateDTO inputModel)
        {
            try
            {
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();
                if (product_data != null)
                {
                    product_data.ProductName = inputModel.ProductName;
                    product_data.RemainingStock = Convert.ToInt32(inputModel.RemainingStock);
                    product_data.ProductPrice = Convert.ToDecimal(inputModel.ProductPrice);
                    product_data.ProductProfit = Convert.ToDecimal(inputModel.ProductProfit);
                    product_data.UpdatedBy = "Admin";
                    product_data.UpdatedDate = DateTime.UtcNow;
                    _unitOfWork.Product.Update(product_data);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();
                if(product_data != null)
                {
                    product_data.ActiveFlag = false;
                    //_unitOfWork.Product.Delete(product_data);
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
