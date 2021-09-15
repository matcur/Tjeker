using System.Threading.Tasks;

namespace Tjeker
{
    public interface IImage
    {
        Task<string> Save();
    }
}