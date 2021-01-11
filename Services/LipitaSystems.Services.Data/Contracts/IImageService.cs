namespace LipitaSystems.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImageService
    {
        Task CreateAsync(string imageUrl, int productId);

        string GetProductTumbnail(int productId);

        string TransformUrlToCropImage(string url);
    }
}
