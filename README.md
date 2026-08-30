[![](https://img.shields.io/nuget/v/soenneker.extensions.cultureinfos.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.cultureinfos/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.cultureinfos/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.cultureinfos/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.cultureinfos.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.cultureinfos/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.cultureinfos/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.cultureinfos/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.CultureInfos

Classifies a `CultureInfo` into a Friday–Saturday or Saturday–Sunday weekend pattern and checks days against that pattern.

## Installation

```bash
dotnet add package Soenneker.Extensions.CultureInfos
```

## Usage

```csharp
using System.Globalization;
using Soenneker.Extensions.CultureInfos;

CultureInfo culture = CultureInfo.GetCultureInfo("ar-SA");

bool usesFridaySaturday = culture.UsesFriSatWeekend();
bool fridayIsWeekend = culture.IsWeekendDay(DayOfWeek.Friday);
IReadOnlySet<DayOfWeek> weekend = culture.GetWeekendDays();
```

`GetWeekendDays()` returns a shared immutable set. It is safe to cache and cannot be cast back to a mutable `HashSet` to alter results globally.

## Classification rules

The Friday–Saturday pattern is selected for:

- Culture names beginning with `ar-`
- `he-IL`
- `fa-IR`
- `ur-PK`

Every other culture uses Saturday–Sunday. Matching is case-insensitive.

These are fixed package rules, not data from an official holiday calendar. They do not account for public holidays, historical changes, employer-specific schedules, or regional exceptions within the same language. For payroll, settlement, compliance, or other date-sensitive business rules, use a maintained calendar source for the relevant jurisdiction.
