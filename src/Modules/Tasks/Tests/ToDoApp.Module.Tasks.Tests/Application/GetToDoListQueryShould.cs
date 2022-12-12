using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Application.Queries;
using ToDoApp.Modules.Tasks.Domain.Entities;
using ToDoApp.Modules.Tasks.Domain.Enums;
using Xunit;

namespace ToDoApp.Module.Tasks.Tests.Application
{
    public class GetToDoListQueryShould
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnUnitValueWhenSuccessful(
            GetTasksQuery query,
            List<ToDoItem> toDoItems,
            [Frozen] Mock<ITasksQueryRepository> todoQueryRepositoryMock,
            GetTaskListQueryHandler sut)
        {
            var expectedItems = toDoItems.Where(x => x.Status != Status.Deleted).ToList();
            todoQueryRepositoryMock.Setup(call => call.GetToDoList(It.IsAny<string>())).ReturnsAsync(toDoItems);

            var result = await sut.Handle(query, CancellationToken.None);

            result.Should().BeEquivalentTo(expectedItems);
            todoQueryRepositoryMock.Verify(call => call.GetToDoList(It.IsAny<string>()), Times.Once);
        }
    }
}
