using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using ToDoApp.Common.Generics;
using ToDoApp.Modules.Users.Application.Commands;
using ToDoApp.Modules.Users.Application.Interfaces;
using Xunit;

namespace ToDoApp.Modules.Users.Tests.Application
{
    public class RegisterUserCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnOkResultWhenSuccessful(
            RegisterUserCommand command,
            [Frozen] Mock<IIdentityService> identityServiceMock,
            RegisterUserCommandHandler sut)
        {
            identityServiceMock.Setup(call => call.RegisterUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Result.Ok());

            var result = await sut.Handle(command, CancellationToken.None);

            identityServiceMock.Verify(call => call.RegisterUser(
                    It.Is<string>(x => x.Equals(command.Email)), It.Is<string>(x => x.Equals(command.Password))),
                Times.Once);
            result.Should().BeEquivalentTo(Result.Ok());
        }
    }
}
