namespace ToDoApp.Modules.Users.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
