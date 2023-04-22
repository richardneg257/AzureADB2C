namespace AzureADB2C.Helpers;

public class B2cCustomAttributeHelper
{
    private readonly string _b2cExtensionAppClientId;

    public B2cCustomAttributeHelper(string b2cExtensionAppClientId)
    {
        _b2cExtensionAppClientId = b2cExtensionAppClientId.Replace("-", "");
    }

    public string GetCompleteAttributeName(string attributeName)
    {
        if (string.IsNullOrWhiteSpace(attributeName))
        {
            throw new ArgumentException("Parameter cannot be null", nameof(attributeName));
        }

        return $"extension_{_b2cExtensionAppClientId}_{attributeName}";
    }
}
