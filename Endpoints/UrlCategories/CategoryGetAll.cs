using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;

namespace API_Coffee.Endpoints.UrlCategories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action( ApplicationDbContext context)
    {
        var categories = context.Categories.ToList();
        var response = categories.Select(c => new CategoryResponse {Id = c.Id, name = c.name, active = c.active });
        return Results.Ok(categories);
    }
}
