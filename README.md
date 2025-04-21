## Challenge Requirements

### Front-end (Angular)

✅ **Display Newest Stories:**
   - A list of the newest stories fetched from the Hacker News API.
   - Each list item should include the story's title and a link to the article (if available). Handle stories that do not have hyperlinks.
   
✅ **Search Functionality:**
   - Provide a search feature that allows users to search for stories by title.

✅ **Pagination:**
   - Implement pagination to ensure that users do not get overloaded with too many stories at once.

✅ **Automated Tests:**
   - Write automated tests to ensure the correctness of your Angular application.

### Back-end (C# .NET Core)

✅ **Dependency Injection:**
   - Use **dependency injection** (which is built into .NET Core) to manage services and components.

✅ **Caching:**
   - Implement caching to improve performance when retrieving the newest stories.

✅ **Automated Tests:**
   - Write automated tests for your API, ensuring both unit and integration tests are included.

### Deployment

✅ **Azure Deployment:**
   - Host your solution on **Azure**.

✅ **GitHub Repository:**
   - Upload the final solution to a **GitHub repository** for review.

✅ **Link Submission:**
   - Provide the link to your GitHub repository and the deployed solution on Azure.

## Readme

### To deploy:
This solution uses Azure CI/CD for Angular and Services [Environment File](.github/workflows/azure-static-web-apps-victorious-tree-00728bb1e.yml)
1. The Angular client is deployed to an Azure Static Web App: https://victorious-tree-00728bb1e.6.azurestaticapps.net/
2. Each API controller is independently deployed to its own Azure App Service.
- NewestStories: https://nextech-hacker-news.azurewebsites.net/api/neweststories?pageIndex=1&pageSize=12

### To run locally:
1. Open a terminal and navigate to the `api\NewestStories` folder, then run: `dotnet run`
2. Open a new terminal, navigate to the `client\` folder, then run: `ng serve`
3. Make sure the API is running at `https://localhost:7128`. If it's not, update the API URL in the Angular environment file:
- Open: `client\src\environments\environment.ts`[Environment File](client\src\environments\environment.ts)
- Modify `apiUrl` to match your API's actual URL, e.g.: 'http://localhost:5168' from launch settings `api\NewestStories\Properties\launchSettings.json`[Environment File](api\NewestStories\Properties\launchSettings.json)

### To execute tests:

1. Open a terminal and navigate to the root folder, then run: `dotnet test`
2. Navigate to the `client\`, then run : `ng test`

### To celebrate Easter:
Psst... there's a little Easter Egg hidden in the contact form. Can you find it? 🐣
