using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CurrencyTracker;

public class CurrencyResponse
{
    [JsonPropertyName("base")]
    public string Base { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; }
}
