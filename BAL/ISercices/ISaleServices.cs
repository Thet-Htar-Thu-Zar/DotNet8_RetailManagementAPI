using MODEL.DTOs;
using MODEL.Entities;

namespace BAL.ISercices
{
    public interface ISaleServices
    {
        Task AddSaleMultiple(IEnumerable<CreateSaleDTO> inputModel);
        Task AddSale(CreateSaleDTO inputModel);
        Task <IEnumerable<SaleReports>> GetSaleReports();
        Task<SaleReports> GetSaleReportById(Guid id);
        Task<SaleSummary> GetAllSaleSummary();
        Task DeleteSale(DeleteSale inputModel);
    }
}
