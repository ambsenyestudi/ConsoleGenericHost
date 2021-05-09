using ConsoleGenericHost.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Application
{
    public interface IPostingRepository
    {
        Task<IEnumerable<PostDTO>> GetAllPosts();
    }
}
