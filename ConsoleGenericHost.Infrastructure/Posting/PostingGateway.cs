using ConsoleGenericHost.Application.DTO;
using ConsoleGenericHost.Application.Posting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleGenericHost.Infrastructure.Posting
{
    public class PostingGateway : IPostingGateway
    {
        private readonly HttpClient httpClient;
        private readonly string postsEndpoint;

        public PostingGateway(HttpClient httpClient, IOptions<PostingSettings> options)
        {
            this.httpClient = httpClient;
            postsEndpoint = options.Value.Endpoint;
        }
        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            var response = await httpClient.GetAsync(postsEndpoint);
            response.EnsureSuccessStatusCode();
            return await DeserializeContent(response.Content);
        }
        public async Task<IEnumerable<PostDTO>> DeserializeContent(HttpContent content)
        {
            var contentStream = await content.ReadAsStreamAsync();
            var resultList = await JsonSerializer.DeserializeAsync<IEnumerable<PostDTO>>(contentStream);
            return resultList;
        }
    }
}
