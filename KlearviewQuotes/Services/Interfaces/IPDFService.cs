using KlearviewQuotes.Models;

namespace KlearviewQuotes.Services.Interfaces
{
    public interface IPDFService
    {
        Task<PDF?> ConvertPreviewToPDF(int id);
    }
}
