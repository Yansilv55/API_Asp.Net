using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;

namespace API_Coffee.Endpoints.UrlCategories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    [Authorize]
    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = new Category(categoryRequest.name, "Test", "Test");

        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

         context.Categories.AddAsync(category);
         context.SaveChangesAsync();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
