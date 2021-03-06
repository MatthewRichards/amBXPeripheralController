﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using aPC.Common.Entities;
using aPC.Common.Builders;
using aPC.Common.Defaults;

namespace aPC.Common.Tests
{
  [TestFixture]
  class FrameStatisticsTests
  {
    [TestFixtureSetUp]
    public void Setup()
    {
      mArbitraryLight = DefaultLights.Red;
    }

    [Test]
    public void FrameStatistics_ContainsExpectedComponentType()
    {
      var lLightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.North, mArbitraryLight)
        .Build();
      var lFrames = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lLightSection)
        .Build();
      var lStats = new FrameStatistics(lFrames);

      Assert.IsTrue(lStats.AreEnabledForComponent(eComponentType.Light));
      Assert.IsFalse(lStats.AreEnabledForComponent(eComponentType.Fan));
      Assert.IsFalse(lStats.AreEnabledForComponent(eComponentType.Rumble));

      Assert.IsTrue(lStats.AreEnabledForComponentAndDirection(eComponentType.Light, eDirection.North));
      Assert.AreEqual(1, lStats.EnabledDirectionalComponents.Count());
    }

    [Test]
    public void FrameStatistics_ContainsNoDuplicates()
    {
      var lLightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithAllLights(mArbitraryLight)
        .Build();
      var lFrames = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lLightSection)
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lLightSection)
        .Build();
      var lStats = new FrameStatistics(lFrames);

      Assert.AreEqual(8, lStats.EnabledDirectionalComponents.Count);
    }

    private Light mArbitraryLight;
  }
}
