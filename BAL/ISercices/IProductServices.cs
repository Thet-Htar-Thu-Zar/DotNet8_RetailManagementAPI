using MODEL.DTOs;
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
        Task UpdateProduct(UpdateProductDTO inputModel);
    }
}
