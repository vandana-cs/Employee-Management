using System.Net.Http.Json;
using EmployeeLeaveManagement.UI.Models;

namespace EmployeeLeaveManagement.UI.Services;

public class LeaveTypeService
{
    private readonly HttpClient _httpClient;

    public LeaveTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LeaveTypeDto>> GetLeaveTypesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LeaveTypeDto>>(
            "api/leave-types") ?? new List<LeaveTypeDto>();
    }
}