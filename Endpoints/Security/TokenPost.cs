using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace API_Coffee.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
    {
       var user = userManager.FindByEmailAsync(loginRequest.email).Result;
        if (user == null)
            Results.BadRequest();
        if(!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();

        var claims = userManager.GetClaimsAsync(user).Result;
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        subject.AddClaims(claims);

        var Key = Encoding.ASCII.GetBytes("A@fderwfqqsdxccer34");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.email),
            }),
            SigningCredentials =
              new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "Coffee-Break",
            Issuer = "Issuer",
            Expires = environment.IsDevelopment() || environment.IsStaging() ?
                DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(2)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}
