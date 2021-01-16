namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task CreateAsync(string imageUrl, int productId);

        Task<string> GetProductTumbnailAsync(int productId);

        string TransformUrlToCropImage(string url);
    }
}
