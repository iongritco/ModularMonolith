﻿using AutoFixture.Xunit2;

using Moq;

using ToDoApp.Common.Tests;
using ToDoApp.Modules.Tasks.Application.Commands.DeleteTask;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;
using ToDoApp.Modules.Tasks.Domain.Enums;

using Xunit;

namespace ToDoApp.Module.Tasks.Tests.Application;

public class DeleteToDoCommandHandlerShould
{
    [Theory]
    [AutoMoqData]
    public async Task UpdateStatusToDeleted(
        ToDoItem toDoItem,
        [Frozen] Mock<ITasksQueryRepository> queryRepositoryMock,
        [Frozen] Mock<ITasksCommandRepository> commandRepositoryMock,
        DeleteToDoCommandHandler sut)
    {
        var command = new DeleteToDoCommand(Guid.NewGuid(), "username");
        queryRepositoryMock.Setup(call => call.GetToDo(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(toDoItem);

        await sut.Handle(command, CancellationToken.None);

        commandRepositoryMock.Verify(call => call.UpdateToDo(It.Is<ToDoItem>(x => x.Status == Status.Deleted)),
            Times.Once);
    }
}
