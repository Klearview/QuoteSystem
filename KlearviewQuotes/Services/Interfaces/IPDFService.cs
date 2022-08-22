namespace KlearviewQuotes.Services.Interfaces
{
    public interface IPDFService
    {
        Task<byte[]?> ConvertPreviewToPDF(int id);
    }
}
