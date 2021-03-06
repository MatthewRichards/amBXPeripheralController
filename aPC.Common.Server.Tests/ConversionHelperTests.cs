﻿using System;
using NUnit.Framework;
using aPC.Common.Server;

namespace aPC.Common.Server.Tests
{
  //qqUMI These tests are dependent on amBXLib => they require ambxrt.
  // It's not possible to include this in source control and they will fail
  // until this file is copied to the appropriate places.
  [TestFixture]
  class ConversionHelperTests
  {
    [Test]
    [TestCaseSource("Directions")]
    public void eDirectionEnum_AgreesExactlyWithCompassDirectionEnum(eDirection xiDirection)
    {
      var lCompassDirection = ConversionHelpers.GetDirection(xiDirection);
      Assert.AreEqual((int)xiDirection, (int)lCompassDirection);
      Assert.AreEqual(xiDirection.ToString(), lCompassDirection.ToString());
    }

    [Test]
    [TestCaseSource("RumbleTypes")]
    public void eRumbletypeEnum_AgreesExactlyWithRumbleTypeEnum(eRumbleType xiRumbleType)
    {
      var lRumbleType = ConversionHelpers.GetRumbleType(xiRumbleType);
      Assert.AreEqual((int)xiRumbleType, (int)lRumbleType);
      Assert.AreEqual(xiRumbleType.ToString(), lRumbleType.ToString());
    }

    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private readonly eRumbleType[] RumbleTypes = (eRumbleType[])Enum.GetValues(typeof(eRumbleType));
  }
}
