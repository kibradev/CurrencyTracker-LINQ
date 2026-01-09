using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTracker;
using CurrencyTracker.Services;

class Program
{
    static async Task Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        var service = new CurrencyService();
        var currencies = await service.GetCurrenciesAsync();

        if (currencies.Count == 0)
        {
            Console.WriteLine("api verisi alinamadi");
            return;
        }

        while (true)
        {
            MenuYazdir();
            var secim = Console.ReadLine()?.Trim();

            switch (secim)
            {
                case "1":
                    TumDovizleriListele(currencies);
                    break;
                case "2":
                    KodaGoreAra(currencies);
                    break;
                case "3":
                   BuyukDegerListele(currencies);
                    break;
                case "4":
                   DegereGoreSirala(currencies);
                    break;
                case "5":
                   IstatistikOzet(currencies);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("yanlis secim");
                    break;
            }
        }
    }

    static void MenuYazdir()
    {
        Console.WriteLine();
        Console.WriteLine("===== CurrencyTracker =====");
        Console.WriteLine("1. Tum dovizleri listele");
        Console.WriteLine("2. Koda gore doviz ara");
        Console.WriteLine("3. Belirli bir degerden buyuk dovizleri listele");
        Console.WriteLine("4. Dovizleri degere gore sirala");
        Console.WriteLine("5. Istatistiksel ozet goster");
        Console.WriteLine("0. Cikis");
        Console.Write("Seciminiz: ");
    }

    static void TumDovizleriListele(List<Currency> currencies)
    {
        var list = currencies
            .Select(x => new { x.Code, x.Rate })
            .OrderBy(x => x.Code)
            .ToList();

        list.ForEach(x => Console.WriteLine($"{x.Code} : {x.Rate}"));
    }

    static void KodaGoreAra(List<Currency> currencies)
    {
        Console.Write("kod gir (orn: USD): ");
        var input = Console.ReadLine()?.Trim() ?? "";

        var found = currencies
            .Where(x => x.Code.Equals(input, StringComparison.OrdinalIgnoreCase))
            .Select(x => new { x.Code, x.Rate })
            .ToList();

        if (found.Count == 0)
        {
            Console.WriteLine("bulunamadi");
            return;
        }

        found.ForEach(x => Console.WriteLine($"{x.Code} : {x.Rate}"));
    }

    static void BuyukDegerListele(List<Currency> currencies)
    {
        Console.Write("esik deger: ");
        var raw = Console.ReadLine()?.Trim() ?? "";

        if (!decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out var threshold))
        {
            Console.WriteLine("sayi gir");
            return;
        }

        var list = currencies
            .Where(x => x.Rate > threshold)
            .OrderByDescending(x => x.Rate)
            .Select(x => new { x.Code, x.Rate })
            .ToList();

        if (list.Count == 0)
        {
            Console.WriteLine("sonuc yok");
            return;
        }

        list.ForEach(x => Console.WriteLine($"{x.Code} : {x.Rate}"));
    }

    static void DegereGoreSirala(List<Currency> currencies)
    {
        Console.Write("1 artan, 2 azalan: ");
        var mode = Console.ReadLine()?.Trim();

        var list = mode == "2"
            ? currencies.OrderByDescending(x => x.Rate).Select(x => new { x.Code, x.Rate }).ToList()
            : currencies.OrderBy(x => x.Rate).Select(x => new { x.Code, x.Rate }).ToList();

        list.ForEach(x => Console.WriteLine($"{x.Code} : {x.Rate}"));
    }

    static void IstatistikOzet(List<Currency> currencies)
    {
        var count = currencies.Count();
        var max = currencies.Max(x => x.Rate);
        var min = currencies.Min(x => x.Rate);
        var avg = currencies.Average(x => x.Rate);

        var maxItem = currencies.Where(x => x.Rate == max).Select(x => x.Code).FirstOrDefault();
        var minItem = currencies.Where(x => x.Rate == min).Select(x => x.Code).FirstOrDefault();

        Console.WriteLine($"toplam doviz sayisi: {count}");
        Console.WriteLine($"en yuksek kur: {maxItem} = {max}");
        Console.WriteLine($"en dusuk kur: {minItem} = {min}");
        Console.WriteLine($"ortalama kur: {avg}");
    }
}
