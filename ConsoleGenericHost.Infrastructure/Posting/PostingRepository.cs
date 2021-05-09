using ConsoleGenericHost.Application;
using ConsoleGenericHost.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Infrastructure
{
    public class PostingRepository : IPostingRepository
    {
        public Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            return Task.FromResult((IEnumerable<PostDTO>)Array.Empty<PostDTO>());
        }
    }
}
