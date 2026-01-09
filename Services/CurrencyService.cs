using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyTracker.Services;

public class CurrencyService
{
    private static readonly HttpClient httpClient = new HttpClient();
    private const string Url = "https://api.frankfurter.app/latest?from=TRY";

    public async Task<List<Currency>> GetCurrenciesAsync()
    {
        using var res = await httpClient.GetAsync(Url);
        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<CurrencyResponse>(json);

        if (data == null || data.Rates == null)
            return new List<Currency>();

        return data.Rates
            .Select(x => new Currency { Code = x.Key, Rate = x.Value })
            .ToList();
    }
}
