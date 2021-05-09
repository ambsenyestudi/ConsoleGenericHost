using ConsoleGenericHost.Application.DTO;
using ConsoleGenericHost.Application.Posting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Infrastructure.Posting
{
    public class PostingRepository : IPostingRepository
    {
        private readonly IPostingGateway postingGateway;

        public PostingRepository(IPostingGateway postingGateway)
        {
            this.postingGateway = postingGateway;
        }
        public Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            return postingGateway.GetAllPosts();
        }
    }
}
