using MediatR;
using Microsoft.Extensions.Logging;
using ToDoApp.Modules.Users.Application.Exceptions;
using ToDoApp.Modules.Users.Application.Interfaces;
using ToDoApp.Modules.Users.Domain.Entities;

namespace ToDoApp.Modules.Users.Application.Queries.GetUserByEmail
{
    public class GetUserByEmailCommandHandler : IRequestHandler<GetUserByEmailCommand, User>
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<GetUserByEmailCommandHandler> _logger;

        public GetUserByEmailCommandHandler(IIdentityService identityService, ILogger<GetUserByEmailCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<User> Handle(GetUserByEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GetUserByEmail(request.Email);
            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Unable to get user {user}; error - {error}", request.Email, result.ErrorMessage);
                throw new UserNotFoundException(request.Email);
            }

            return result.Payload;
        }
    }
}
