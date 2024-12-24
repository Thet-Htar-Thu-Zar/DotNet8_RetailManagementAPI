using BAL.ISercices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;
using REPOSITORY.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class SaleServices : ISaleServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddSale(CreateSaleDTO inputModel)
        {
            try
            {
                var addsale = new SaleReports()
                {
                    ProductID = inputModel.ProductID,
                    QuantitySold = inputModel.QuantitySold,
                    TotalPrice = inputModel.TotalPrice,
                    TotalProfit = inputModel.TotalProfit,
                    CreatedBy = inputModel.CreatedBy,
                };

                await _unitOfWork.Sale.Add(addsale);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
