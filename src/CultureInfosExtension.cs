using Soenneker.Extensions.String;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Soenneker.Extensions.CultureInfos;

/// <summary>
/// A collection of helpful CultureInfo extension methods
/// </summary>
public static class CultureInfosExtension
{
    private static readonly HashSet<DayOfWeek> _friSat =
    [
        DayOfWeek.Friday,
        DayOfWeek.Saturday
    ];

    private static readonly HashSet<DayOfWeek> _satSun =
    [
        DayOfWeek.Saturday,
        DayOfWeek.Sunday
    ];

    /// <summary>Returns <c>true</c> if the culture’s weekend is <b>Friday + Saturday</b>.</summary>
    [Pure]
    public static bool UsesFriSatWeekend(this CultureInfo culture)
    {
        string name = culture.Name;

        return name.StartsWithIgnoreCase("ar-") || // Arabic locales
               name.EqualsIgnoreCase("he-IL") || // Israel
               name.EqualsIgnoreCase("fa-IR") || // Iran
               name.EqualsIgnoreCase("ur-PK"); // Pakistan
    }

    /// <summary>Fast check—no heap work.</summary>
    [Pure]
    public static bool IsWeekendDay(this CultureInfo culture, DayOfWeek day) => culture.UsesFriSatWeekend()
        ? day is DayOfWeek.Friday or DayOfWeek.Saturday
        : day is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// Shared, read‑only weekend set (treat as immutable!).
    /// No per‑call allocations; returns a cached <see cref="IReadOnlySet{T}"/>.
    /// </summary>
    [Pure]
    public static IReadOnlySet<DayOfWeek> GetWeekendDays(this CultureInfo culture) => culture.UsesFriSatWeekend() ? _friSat : _satSun;
}