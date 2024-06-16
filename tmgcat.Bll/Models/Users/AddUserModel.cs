namespace tmgcat.Bll.Models.Users;

public class AddUserModel
{
    public required string Password { get; init; }
    public string? Name { get; init; }
    public required string Email { get; init; }
}