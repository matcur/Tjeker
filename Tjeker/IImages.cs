using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tjeker
{
    public interface IImages
    {
        Task<IEnumerable<string>> Save();
    }
}