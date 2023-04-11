namespace ECommerceAPI.Infrastructure.Operations;

public static class NameOperation
{
    public static string CharacterRegulator(string name)
    {
        return name.Replace(" ", "-");
    }
}