using AutoFixture.Xunit2;
using FluentAssertions;
using MediatR;
using Moq;
using ToDoApp.Modules.Tasks.Application.Commands.UpdateTask;
using ToDoApp.Modules.Tasks.Application.Events;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;
using Xunit;

namespace ToDoApp.Module.Tasks.Tests.Application
{
    public class UpdateToDoCommandHandlerShould
    {
        [Theory]
        [AutoMoqData]
        public async Task UpdateStatusToDeletedAndPublishUpdateEvent(
            ToDoItem toDoItem,
            UpdateTaskCommand command,
            [Frozen] Mock<ITasksQueryRepository> queryRepositoryMock,
            [Frozen] Mock<ITasksCommandRepository> commandRepositoryMock,
            [Frozen] Mock<IMediator> mediator,
            UpdateToDoCommandHandler sut)
        {
            queryRepositoryMock.Setup(call => call.GetToDo(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(toDoItem);

            var result = await sut.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
            commandRepositoryMock.Verify(
                call => call.UpdateToDo(It.Is<ToDoItem>(x =>
                    x.Status == command.Status && x.Description == command.Description)),
                Times.Once);
            mediator.Verify(call => call.Publish(It.IsAny<TaskUpdatedEvent>(), CancellationToken.None), Times.Once);
        }
    }
}
