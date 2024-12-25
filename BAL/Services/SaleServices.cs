using BAL.ISercices;
using MODEL.DTOs;
using MODEL.Entities;
using REPOSITORY.UnitOfWork;

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
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();
                if (product_data != null)
                {
                    product_data.RemainingStock -= inputModel.QuantitySold;
                    _unitOfWork.Product.Update(product_data);

                    var addsale = new SaleReports()
                    {
                        ProductID = inputModel.ProductID,
                        QuantitySold = inputModel.QuantitySold,
                        TotalPrice = product_data.ProductPrice * inputModel.QuantitySold,
                        TotalProfit = product_data.ProductProfit * inputModel.QuantitySold,
                        CreatedBy = inputModel.CreatedBy,
                    };

                    await _unitOfWork.Sale.Add(addsale);
                    await _unitOfWork.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SaleReports>> GetSaleReports()
        {
            try
            {
                var lst = await _unitOfWork.Sale.GetAll();
                if(lst is null)
                {
                    throw new Exception("No item in sale report..");
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SaleReports> GetSaleReportById(Guid id)
        {
            try
            {
                var saleReport = (await _unitOfWork.Sale.GetByCondition(x => x.SaleID == id)).FirstOrDefault();

                if(saleReport is null)
                {
                    throw new Exception("No data found");
                }

                return saleReport;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
