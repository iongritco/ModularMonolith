using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ToDoApp.Modules.Users.API.Infrastructure.Interfaces;
using ToDoApp.Modules.Users.API.Models;

namespace ToDoApp.Modules.Users.API.Infrastructure;

public class JwtTokenService : ITokenService
{
	private readonly Settings _settings;

	public JwtTokenService(Settings settings)
	{
		_settings = settings;
	}

	public string GenerateToken(string username)
	{
		var symmetricKey = Convert.FromBase64String(_settings.TokenKey);
		var tokenHandler = new JwtSecurityTokenHandler();

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
						{
								new Claim(ClaimTypes.Name, username)
							}),
			Expires = DateTime.UtcNow.AddDays(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
		};

		var stoken = tokenHandler.CreateToken(tokenDescriptor);
		var token = tokenHandler.WriteToken(stoken);

		return token;
	}
}
