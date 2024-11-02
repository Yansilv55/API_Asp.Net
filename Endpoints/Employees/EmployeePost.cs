using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Xml.Linq;

namespace API_Coffee.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeRequest.email, Email = employeeRequest.email };
        var result = userManager.CreateAsync(user, employeeRequest.password).Result;

        if (!result.Succeeded)
        {
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());
        }

        var userClaims = new List<Claim>()
    {
        new Claim("EmployeeCode", employeeRequest.EmployeeCode),
        new Claim("name", employeeRequest.name)
    };


        var claimResult
            = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.First());
        }

        return Results.Created($"/employee/{user.Id}", user.Id);
    }
}
