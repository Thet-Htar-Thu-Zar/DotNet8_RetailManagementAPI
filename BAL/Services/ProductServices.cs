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

        public async Task AddProduct(AddProductDTO inputModel)
        {
            try
            {
                var addproduct = new Products()
                {
                    ProductName = inputModel.ProductName,
                    RemainingStock = inputModel.RemainingStock,
                    ProductPrice = inputModel.ProductPrice,
                    ProductProfit = inputModel.ProductProfit,
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

        public async Task UpdateProduct (UpdateProductDTO inputModel)
        {
            try
            {
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();
                if (product_data != null)
                {
                    product_data.ProductName = inputModel.ProductName;
                    product_data.RemainingStock = inputModel.RemainingStock;
                    product_data.ProductPrice = inputModel.ProductPrice;
                    product_data.ProductProfit = inputModel.ProductProfit;
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
    }
}
