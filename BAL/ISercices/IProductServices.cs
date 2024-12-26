using MODEL.DTOs;
using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
