using MediatR;
using ToDoApp.Modules.Users.Application.Interfaces;

namespace ToDoApp.Modules.Users.Application.Commands.UpdateNumberOfTasks
{
    public class UpdateNumberOfTasksCommandHandler : IRequestHandler<UpdateNumberOfTasksCommand>
    {
        private readonly IIdentityService _identityService;

        public UpdateNumberOfTasksCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Handle(UpdateNumberOfTasksCommand request, CancellationToken cancellationToken)
        {
            await _identityService.UpdateNumberOfTasks(request.Email);
        }
    }
}
