using System.Net.Http.Json;
using EmployeeLeaveManagement.UI.Models;

namespace EmployeeLeaveManagement.UI.Services;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<EmployeeDto>> GetEmployeesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>(
            "api/employees") ?? new List<EmployeeDto>();
    }

    public async Task<EmployeeDto?> GetEmployeeAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<EmployeeDto>(
            $"api/employees/{id}");
    }

    public async Task<bool> CreateEmployeeAsync(CreateEmployeeDto employee)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/employees",
            employee);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeactivateEmployeeAsync(int id)
    {
        var response = await _httpClient.PutAsync(
            $"api/employees/{id}/deactivate",
            null);

        return response.IsSuccessStatusCode;
    }
}