using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using Soenneker.Extensions.String;

namespace Soenneker.Extensions.CultureInfos;

/// <summary>
/// Provides fixed weekend-pattern helpers for cultures.
/// </summary>
public static class CultureInfosExtension
{
    private static readonly FrozenSet<DayOfWeek> _friSat = new[]
    {
        DayOfWeek.Friday,
        DayOfWeek.Saturday
    }.ToFrozenSet();

    private static readonly FrozenSet<DayOfWeek> _satSun = new[]
    {
        DayOfWeek.Saturday,
        DayOfWeek.Sunday
    }.ToFrozenSet();

    /// <summary>Returns whether the culture maps to the library's Friday–Saturday weekend pattern.</summary>
    /// <param name="culture">The culture to classify.</param>
    /// <returns><c>true</c> for names beginning with <c>ar-</c> and for <c>he-IL</c>, <c>fa-IR</c>, or <c>ur-PK</c>; otherwise <c>false</c>.</returns>
    [Pure]
    public static bool UsesFriSatWeekend(this CultureInfo culture)
    {
        string name = culture.Name;

        return name.StartsWithIgnoreCase("ar-") ||
               name.EqualsIgnoreCase("he-IL") ||
               name.EqualsIgnoreCase("fa-IR") ||
               name.EqualsIgnoreCase("ur-PK");
    }

    /// <summary>Returns whether a day belongs to the culture's mapped weekend pattern.</summary>
    /// <param name="culture">The culture to classify.</param>
    /// <param name="day">The day to check.</param>
    /// <returns><c>true</c> when the day belongs to the selected Friday–Saturday or Saturday–Sunday pattern.</returns>
    [Pure]
    public static bool IsWeekendDay(this CultureInfo culture, DayOfWeek day) => culture.UsesFriSatWeekend()
        ? day is DayOfWeek.Friday or DayOfWeek.Saturday
        : day is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// Returns the immutable, shared weekend set selected for the culture.
    /// </summary>
    /// <param name="culture">The culture to classify.</param>
    /// <returns>A cached Friday–Saturday or Saturday–Sunday set.</returns>
    [Pure]
    public static IReadOnlySet<DayOfWeek> GetWeekendDays(this CultureInfo culture) => culture.UsesFriSatWeekend() ? _friSat : _satSun;
}
