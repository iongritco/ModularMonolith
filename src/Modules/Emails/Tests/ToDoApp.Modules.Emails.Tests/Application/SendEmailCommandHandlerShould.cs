using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Moq;
using ToDoApp.Common.Tests;
using ToDoApp.Modules.Emails.Application.Commands.SendEmail;
using ToDoApp.Modules.Emails.Application.Interfaces;
using ToDoApp.Modules.Emails.Domain.Entities;
using Xunit;

namespace ToDoApp.Modules.Emails.Tests.Application
{
    public class SendEmailCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnUnitValueWhenSuccessful(
            SendEmailCommand command,
            [Frozen] Mock<IEmailService> todoCommandRepositoryMock,
            SendEmailCommandHandler sut)
        {
            await sut.Handle(command, CancellationToken.None);

            todoCommandRepositoryMock.Verify(call => call.SendEmail(
                It.Is<Email>(x => x.To.Equals(command.Email) && x.Body.Equals(command.Description))),
                Times.Once);
        }
    }
}
