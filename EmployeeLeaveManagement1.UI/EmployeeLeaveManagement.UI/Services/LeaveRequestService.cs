using System.Net.Http.Json;
using EmployeeLeaveManagement.UI.Models;

namespace EmployeeLeaveManagement.UI.Services;

public class LeaveRequestService
{
    private readonly HttpClient _httpClient;

    public LeaveRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ApplyLeaveAsync(
        CreateLeaveRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/leave-requests",
            request);

        return response.IsSuccessStatusCode;
    }

    public async Task<LeaveRequestDto?> GetLeaveRequestAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<LeaveRequestDto>(
            $"api/leave-requests/{id}");
    }

    public async Task<List<LeaveRequestDto>> GetEmployeeLeaveRequestsAsync(
        int employeeId)
    {
        return await _httpClient.GetFromJsonAsync<List<LeaveRequestDto>>(
            $"api/employees/{employeeId}/leave-requests")
            ?? new List<LeaveRequestDto>();
    }

    public async Task<bool> ApproveLeaveAsync(int id)
    {
        var response = await _httpClient.PutAsync(
            $"api/leave-requests/{id}/approve",
            null);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RejectLeaveAsync(int id)
    {
        var response = await _httpClient.PutAsync(
            $"api/leave-requests/{id}/reject",
            null);

        return response.IsSuccessStatusCode;
    }
}