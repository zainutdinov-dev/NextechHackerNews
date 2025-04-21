using Microsoft.AspNetCore.Mvc;
using NewestStories.Models.Dto;
using NewestStories.Services.Interfaces;

namespace NewestStories.Controllers
{
    /// <summary>
    /// Controller for handling HTTP requests related to newest Hacker News stories.
    /// </summary>
    /// <remarks>
    /// This controller provides an endpoint to fetch a paginated and optionally filtered list of the newest stories.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class NewestStoriesController : ControllerBase
    {
        private readonly INewestStoriesService newestStoriesService;

        public NewestStoriesController(INewestStoriesService newestStoriesService)
        {
            this.newestStoriesService = newestStoriesService;
        }

        /// <summary>
        /// Retrieves a paginated list of the newest stories.
        /// </summary>
        /// <param name="requestDto">Query parameters including pageIndex, pageSize, and optional search text.</param>
        /// <returns>HTTP 200 with a list of stories if successful.</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]NewestStoriesRequestDto requestDto)
        {
            var responseDto = await newestStoriesService.GetNewestStoriesAsync(requestDto);

            return Ok(responseDto);
        }
    }
}
