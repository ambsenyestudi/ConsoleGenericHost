using ConsoleGenericHost.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Application.Posting
{
    public interface IPostingGateway
    {
        Task<IEnumerable<PostDTO>> GetAllPosts();
    }
}
