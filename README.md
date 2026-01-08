# CurrencyTracker

CurrencyTracker is a C# console application that retrieves exchange rates based on Turkish Lira (TRY) using the Frankfurter FREE API.

## Technologies
- C#
- .NET Console Application
- HttpClient
- async / await
- LINQ

## Features
- List all currencies
- Search currency by code
- List currencies above a specific value
- Sort currencies by rate
- Show statistical summary (count, max, min, average)

## Data Source
https://api.frankfurter.app/latest?from=TRY

## Notes
- Exchange rate data is fetched dynamically from the API
- All operations are performed using LINQ
- No hard-coded currency data is used
