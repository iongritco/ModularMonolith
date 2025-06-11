using MediatR;
using ToDoApp.Modules.Users.API.Infrastructure.Interfaces;

namespace ToDoApp.Modules.Users.API.Features.UpdateNumberOfTasks;

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
