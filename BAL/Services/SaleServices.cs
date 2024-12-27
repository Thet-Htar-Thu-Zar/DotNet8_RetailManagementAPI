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
        public async Task AddSaleMultiple(IEnumerable<CreateSaleDTO> inputModel)
        {
            try
            {
                foreach(var sale in inputModel)
                {
                    var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == sale.ProductID)).FirstOrDefault();

                    if (product_data != null)
                    {
                        if (product_data.ActiveFlag != true)
                        {
                            throw new Exception("Product doesn't find...");
                        }

                        if (product_data.RemainingStock < sale.QuantitySold)
                        {
                            throw new Exception("Remaining Stock is not enough..");
                        }

                        if(sale.QuantitySold <= 0)
                        {
                            throw new Exception("Quantity is more than 0...");
                        }
                        product_data.RemainingStock -= sale.QuantitySold;
                        _unitOfWork.Product.Update(product_data);

                        await _unitOfWork.Sale.Add(new SaleReports()
                        {
                            ProductID = sale.ProductID,
                            QuantitySold = sale.QuantitySold,
                            TotalPrice = Convert.ToDecimal(product_data.ProductPrice * sale.QuantitySold),
                            TotalProfit = Convert.ToDecimal(product_data.ProductProfit * sale.QuantitySold),
                            CreatedBy = sale.CreatedBy,
                        });

                    }
                }
                        await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddSale(CreateSaleDTO inputModel)
        {
            try
            {
                var product_data = (await _unitOfWork.Product.GetByCondition(x => x.ProductID == inputModel.ProductID)).FirstOrDefault();

                if (product_data != null)
                {
                    if (product_data.ActiveFlag != true)
                    {
                        throw new Exception("Product doesn't find...");
                    }

                    if (product_data.RemainingStock < inputModel.QuantitySold)
                    {
                        throw new Exception("Remaining Stock is not enough...");
                    }

                    if(inputModel.QuantitySold <= 0)
                    {
                        throw new Exception("Quantity is more than O...");
                    }

                    product_data.RemainingStock -= inputModel.QuantitySold;
                    _unitOfWork.Product.Update(product_data);

                    var addsale = new SaleReports()
                    {
                        ProductID = inputModel.ProductID,
                        QuantitySold = inputModel.QuantitySold,
                        TotalPrice = Convert.ToDecimal(product_data.ProductPrice * inputModel.QuantitySold),
                        TotalProfit = Convert.ToDecimal(product_data.ProductProfit * inputModel.QuantitySold),
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
                var lst = await _unitOfWork.Sale.GetByCondition(x => x.ActiveFlag == true);

                if (lst == null || !lst.Any())
                {
                    throw new Exception("No active items in the sale report.");
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
                    throw new Exception("Sale report doesn't exist....");
                }
                if (saleReport.ActiveFlag != true)
                {
                    throw new Exception("Sale report doesn't exist....");

                }

                return saleReport;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SaleSummary> GetAllSaleSummary()
        {
            try
            {
                var salereport = await _unitOfWork.Sale.GetAll();

                if (salereport is null)
                {
                    throw new Exception("Sale report don't find");
                }
                else
                {
                    var totalSaleSummary = new SaleSummary();

                    foreach (var report in salereport)
                    {
                        totalSaleSummary.TotalSaleRevenue += Convert.ToDecimal(report.TotalPrice);
                        totalSaleSummary.TotalSaleProfit += Convert.ToDecimal(report.TotalProfit);
                    
                    };

                    return totalSaleSummary;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteSale(DeleteSale inputModel)
        {
            try
            {
                var salereport = (await _unitOfWork.Sale.GetByCondition(x => x.SaleID == inputModel.SaleID)).FirstOrDefault();

                if (salereport is not null)
                {
                    salereport.ActiveFlag = false;
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SaleReports>> GetAllSaleReportsWithPagination(int page, int pageSize)
        {
            try
            {
                var lst = await _unitOfWork.Sale.GetByCondition(x => x.ActiveFlag == true);
                var totalCount = lst.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (page < 1 || page > totalPages)
                {
                    throw new Exception($"Invalid page number.. The page should be between 1 and {pageSize}");
                }

                var lstPerPage = lst.Skip((page - 1) * pageSize).Take(pageSize).Take(pageSize);

                if (!lstPerPage.Any())
                {
                    throw new Exception("No Sale List on page");
                }

                return lstPerPage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
