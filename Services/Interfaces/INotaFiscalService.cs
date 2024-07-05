using TechNationFinanceiroApi.Models;

public interface INotaFiscalService
{
    Task<IEnumerable<NotaFiscal>> GetNotasFiscais();
    Task<NotaFiscal> GetNotaFiscal(int id);
    Task<NotaFiscal> PostNotaFiscal(NotaFiscal notaFiscal);
    Task<bool> PutNotaFiscal(int id, NotaFiscal notaFiscal);
    Task<bool> DeleteNotaFiscal(int id);
}
