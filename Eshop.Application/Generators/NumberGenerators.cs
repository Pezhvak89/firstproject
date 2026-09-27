namespace Eshop.Application.Generators;

public static class NumberGenerators
{
    public static int RandomNumberGenerator()
    {
        var random = new Random();
        int randomNumber = random.Next(111111,999999);
        return randomNumber;
    }
    
}