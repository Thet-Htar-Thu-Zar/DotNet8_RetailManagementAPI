using MODEL.DTOs;
using MODEL.Entities;

namespace BAL.ISercices
{
    public interface ISaleServices
    {
        Task AddSale(CreateSaleDTO inputModel);
        Task <IEnumerable<SaleReports>> GetSaleReports();
        Task<SaleReports> GetSaleReportById(Guid id);
    }
}
