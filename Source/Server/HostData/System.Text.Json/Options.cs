using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HostData.System.Text.Json;

public static class Options
{
    private static JsonSerializerOptions _jsonSerializerOptions;

    public static JsonSerializerOptions JsonSerializerOptions => _jsonSerializerOptions ??= CreateSerializerOptions();

    private static JsonSerializerOptions CreateSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true
        };
        return options;
    }
}