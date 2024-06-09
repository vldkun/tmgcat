namespace tmgcat.Bll.Models.Users;

public class GetUserLightModel
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public string? ProfilePicturePath { get; init; }
}