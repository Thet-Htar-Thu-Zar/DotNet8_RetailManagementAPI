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

        //public async Task GetProductById(Guid id)
        //{
        //    try
        //    {
        //        var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == id)).FirstOrDefault();
        //        if (product_data != null)
        //        {
        //            await _unitOfWork.Product.GetById(id);
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
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID && x.ActiveFlag == true)).FirstOrDefault();

                if (product_data != null)
                {
                    if(!string.IsNullOrEmpty(inputModel.ProductName))
                    {
                        product_data.ProductName = inputModel.ProductName;
                    }
                    if(inputModel.RemainingStock != null)
                    {
                        product_data.RemainingStock = Convert.ToInt32(inputModel.RemainingStock);
                    }
                    if (inputModel.ProductPrice != null)
                    {
                        product_data.ProductPrice = Convert.ToDecimal(inputModel.ProductPrice);

                    }
                    if(inputModel.ProductProfit  != null)
                    {
                        product_data.ProductProfit = Convert.ToDecimal(inputModel.ProductProfit);
                    }
                    if(inputModel.UpdatedBy == null)
                    {
                        product_data.UpdatedBy = "Admin";
                    }
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
                    _unitOfWork.Product.Update(product_data);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }    
    }
}
