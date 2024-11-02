﻿using API_Coffee.Domain.Produto;
using API_Coffee.Infra.Data;
using Coffee_Break.Domain.Produto;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using System.Xml.Linq;

namespace API_Coffee.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;
    [Authorize(Policy = "EmployeePolicy")]
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }
}
