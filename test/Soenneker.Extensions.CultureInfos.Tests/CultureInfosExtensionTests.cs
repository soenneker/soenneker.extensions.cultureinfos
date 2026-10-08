using System;
using System.Collections.Generic;
using System.Globalization;
using Soenneker.Tests.Unit;
using System.Threading;

namespace Soenneker.Extensions.CultureInfos.Tests;

public sealed class CultureInfosExtensionTests : UnitTest
{
    [Test]
    public async global::System.Threading.Tasks.Task GetWeekendDays_does_not_expose_mutable_shared_hashset(CancellationToken cancellationToken)
    {
        IReadOnlySet<DayOfWeek> days = CultureInfo.GetCultureInfo("en-US").GetWeekendDays();

        await Assert.That(days is HashSet<DayOfWeek>).IsFalse();
        await Assert.That(days.Contains(DayOfWeek.Saturday)).IsTrue();
        await Assert.That(days.Contains(DayOfWeek.Sunday)).IsTrue();
    }

    [Test]
    public async global::System.Threading.Tasks.Task Arabic_culture_uses_friday_saturday_pattern(CancellationToken cancellationToken)
    {
        CultureInfo culture = CultureInfo.GetCultureInfo("ar-SA");

        await Assert.That(culture.IsWeekendDay(DayOfWeek.Friday)).IsTrue();
        await Assert.That(culture.IsWeekendDay(DayOfWeek.Sunday)).IsFalse();
    }
}
