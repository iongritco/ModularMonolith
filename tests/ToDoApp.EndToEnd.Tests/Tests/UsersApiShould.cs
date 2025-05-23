using FluentAssertions;
using Xunit;
using static ToDoApp.Tests.EndToEnd.BaseTestsClass;

namespace ToDoApp.Tests.EndToEnd.Tests;

// Login and register are covered as part of setup
[Collection("Tests collection")]
public class UsersApiShould
{
    [Fact]
    public async Task ReturnCurrentUserName()
    {
        // Act
        var result = await CustomHttpClient.GetAsync("api/users/me/name");
        var username = await result.Content.ReadAsStringAsync();

        // Assert
        username.Should().BeEquivalentTo(Username);
    }
}
