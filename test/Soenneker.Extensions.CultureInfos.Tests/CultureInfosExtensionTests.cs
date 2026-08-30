using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Soenneker.Tests.Unit;

namespace Soenneker.Extensions.CultureInfos.Tests;

public sealed class CultureInfosExtensionTests : UnitTest
{
    [Test]
    public async Task GetWeekendDays_does_not_expose_mutable_shared_hashset()
    {
        IReadOnlySet<DayOfWeek> days = CultureInfo.GetCultureInfo("en-US").GetWeekendDays();

        await Assert.That(days is HashSet<DayOfWeek>).IsFalse();
        await Assert.That(days.Contains(DayOfWeek.Saturday)).IsTrue();
        await Assert.That(days.Contains(DayOfWeek.Sunday)).IsTrue();
    }

    [Test]
    public async Task Arabic_culture_uses_friday_saturday_pattern()
    {
        CultureInfo culture = CultureInfo.GetCultureInfo("ar-SA");

        await Assert.That(culture.IsWeekendDay(DayOfWeek.Friday)).IsTrue();
        await Assert.That(culture.IsWeekendDay(DayOfWeek.Sunday)).IsFalse();
    }
}
