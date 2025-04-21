using NewestStories.Integration.Tests.Fakers;
using NewestStories.Integration.Tests.WebApplicationFactories;
using NewestStories.Models.Dto;
using NewestStories.Models.HackerNewsAPI;
using Newtonsoft.Json;

using Moq;
using System.Net;

namespace NewestStories.Integration.Tests
{
    public class NewestStoriesControllerTest : IClassFixture<HackerNewsWebApplicationFactory>
    {
        private readonly HackerNewsWebApplicationFactory factory;

        public NewestStoriesControllerTest(HackerNewsWebApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData(20, 1, 10)]
        [InlineData(0, 1, 10)]
        public async Task Get_ShouldReturnStories(int itemsCount, int pageIndex, int pageSize)
        {
            var items = new HackerNewsStoryFaker().Generate(itemsCount);

            factory.HackerNewsClientMock.Setup(client => client.GetAsync<List<int>>("newstories.json"))
                .ReturnsAsync(items.Select(q => q.id).ToList());

            foreach (var item in items)
            {
                factory.HackerNewsClientMock.Setup(client => client.GetAsync<HackerNewsStory>($"item/{item.id}.json"))
                    .ReturnsAsync(items.Single(q => q.id == item.id));
            }

            var client = factory.CreateClient();

            var response = await client.GetAsync($"/api/neweststories?pageIndex={pageIndex}&pageSize={pageSize}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<NewestStoriesResponseDto>(content);

            Assert.NotNull(result);

            Assert.Equal(pageIndex, result.PageIndex);
            Assert.Equal(Math.Min(itemsCount, pageSize), result.Stories.Count);
            Assert.Equal(itemsCount, result.TotalItemsCount);
            Assert.Equal((int)Math.Ceiling(itemsCount / (float)pageSize), result.TotalPages);

            var expectedStories = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            Assert.Equal(expectedStories.Count, result.Stories.Count);

            for (int i = 0; i < expectedStories.Count; i++)
            {
                var expectedStory = expectedStories[i];
                var story = result.Stories[i];

                Assert.Equal(expectedStory.id, story.Id);
                Assert.Equal(expectedStory.title, story.Title);
                Assert.Equal(expectedStory.url ?? "", story.Url);
            }
        }

        [Theory]
        [InlineData("/api/neweststories?pageIndex=-1&pageSize=0")]
        [InlineData("/api/neweststories?pageIndex=1&pageSize=0")]
        [InlineData("/api/neweststories?pageIndex=-1&pageSize=1")]
        [InlineData("/api/neweststories?pageSize=1")]
        [InlineData("/api/neweststories?pageIndex=-1")]
        [InlineData("/api/neweststories")]
        public async Task Get_ShouldReturnBadRequest_WhenInvalidParameters(string path)
        {
            var client = factory.CreateClient();
            
            var response = await client.GetAsync(path);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
