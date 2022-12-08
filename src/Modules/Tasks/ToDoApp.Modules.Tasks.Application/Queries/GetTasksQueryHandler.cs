using MediatR;
using ToDoApp.Modules.Tasks.Application.Interfaces;
using ToDoApp.Modules.Tasks.Domain.Entities;
using ToDoApp.Modules.Tasks.Domain.Enums;

namespace ToDoApp.Modules.Tasks.Application.Queries
{
    public class GetTaskListQueryHandler : IRequestHandler<GetTasksQuery, List<ToDoItem>>
    {
        private readonly IToDoQueryRepository _queryRepository;

        public GetTaskListQueryHandler(IToDoQueryRepository toDoRepository)
        {
            _queryRepository = toDoRepository;
        }

        public async Task<List<ToDoItem>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var toDoList = await _queryRepository.GetToDoList(request.Username);
            return toDoList.Where(x => x.Status != Status.Deleted).ToList();
        }
    }
}
