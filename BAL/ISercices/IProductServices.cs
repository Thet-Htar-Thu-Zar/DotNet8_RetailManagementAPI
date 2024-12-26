using MODEL.DTOs;

namespace BAL.ISercices
{
    public interface IProductServices
    {
        Task AddProduct(AddProductDTO inputModel);
        Task UpdateProduct(UpdateDTO inputModel);
        Task DeleteProduct(DeleteProductDTO inputModel);
        //Task GetProductById(Guid id);
    }
}
