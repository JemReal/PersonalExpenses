using PersonalExpenses.API.Models.Domain;

namespace PersonalExpenses.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
