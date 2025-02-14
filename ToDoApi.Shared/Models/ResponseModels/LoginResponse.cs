using System;

namespace ToDoApi.Shared.Models.ResponseModels;

public class LoginResponse
{
    public required string Token { get; set; }
}
