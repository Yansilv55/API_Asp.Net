using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;
using Microsoft.AspNetCore.Mvc;

namespace API_Coffee.Endpoints.UrlCategories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute]Guid id, CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if(category == null)
            return Results.NotFound();

        //category.EditInfo(categoryRequest.name, categoryRequest.active);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();
        return Results.Ok();
    }
}
