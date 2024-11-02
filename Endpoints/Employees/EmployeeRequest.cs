namespace API_Coffee.Endpoints.Employees
{
    public record EmployeeRequest(string email, string password,string name, string EmployeeCode)
    {
    }
}
