namespace WebApplicationRabbitMqSignalR.Helpers;

public static class ParseHelper
{
    public static bool? ParseBool(string value)
    {
        bool? parsedBool = null;

        string[] trueValues = ["true", "1", "y", "yes", "on"];
        string[] falseValues = ["false", "0", "n", "no", "off"];

        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        if (trueValues.Any(trueValue => trueValue.ToUpperInvariant() == value.ToUpperInvariant()))
        {
            parsedBool = true;
        }
        else if (falseValues.Any(falseValue => falseValue.ToUpperInvariant() == value.ToUpperInvariant()))
        {
            parsedBool = false;
        }
        
        return parsedBool;
    }

    public static bool ParseBool(ulong value) =>
        value == 1;

}