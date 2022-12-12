using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Moq;
using ToDoApp.Modules.Tasks.Application.Commands.CreateTask;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;
using Xunit;

namespace ToDoApp.Module.Tasks.Tests.Application
{
    public class CreateToDoCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnUnitValueWhenSuccessful(
            CreateTaskCommand command,
            [Frozen] Mock<ITasksCommandRepository> todoCommandRepositoryMock,
            CreateToDoCommandHandler sut)
        {
            var result = await sut.Handle(command, CancellationToken.None);

            todoCommandRepositoryMock.Verify(call => call.CreateToDo(
                It.Is<ToDoItem>(x => x.Id.Equals(command.Id) && x.Description.Equals(command.Description) && x.Username.Equals(command.Username))),
                Times.Once);
            result.Should().Be(Unit.Value);
        }
    }
}
