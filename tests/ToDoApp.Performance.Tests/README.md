# Performance/Load Tests
The scope of these tests is to do performance/load tests on the API on several scenarios - add tasks, 5 requests per second; get tasks - 20 requests per second; get user - 5 requests per second. Of course this configurations can be changed.
* The reason these tests are outside of the main solution is because you'll need two VS instances to run both the API and the tests, since in tests, you're making HTTP requests to the API. 

## Setup
- Before starting, you need to generate a JWT token by using the api/users/login endpoint and update it in the performance tests
- If needed, update also the base path of the API
- It's also recommended to use a separate DB for the performance tests since they will add multiple tasks for the same user - up to 600 with the default configurations (update appsettings in the API project)

## Run tests
- It's important to run the tests using Release mode - you can do it directly in VS or by using dotnet run
- Immediatelly after the run, the report will be opened automatically
- In the report, you'll find multiple metrics like average response time and the status of the requests