﻿using MediatR;
using ToDoApp.Common.Generics;

namespace ToDoApp.Modules.Users.Application.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Result>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
