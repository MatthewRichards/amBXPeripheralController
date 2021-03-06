﻿using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Common.Server.Tests
{
  class TestEngineManager : IEngine
  {
    public TestEngineManager()
    {
      Status = new Frame()
      {
        Lights = new LightSection(),
        Fans = new FanSection(),
        Rumbles = new RumbleSection()
      };
      Updated = new Dictionary<eComponentType, bool>()
        {
          {eComponentType.Light, false}, {eComponentType.Fan, false}, {eComponentType.Rumble, false}
        };
    }

    public void UpdateLight(eDirection xiDirection, Light xiLight, int xiFadeTime)
    {
      Updated[eComponentType.Light] = true;
      Status.Lights.SetComponentValueInDirection(xiLight, xiDirection);
      Status.Lights.FadeTime = xiFadeTime;
    }

    public void UpdateFan(eDirection xiDirection, Fan xiFan)
    {
      Updated[eComponentType.Fan] = true;
      Status.Fans.SetComponentValueInDirection(xiFan, xiDirection);

    }

    public void UpdateRumble(eDirection xiDirection, Rumble xiRumble)
    {
      Updated[eComponentType.Rumble] = true;
      Status.Rumbles.SetComponentValueInDirection(xiRumble, xiDirection);
    }

    public Frame Status;
    public Dictionary<eComponentType, bool> Updated;
  }
}
