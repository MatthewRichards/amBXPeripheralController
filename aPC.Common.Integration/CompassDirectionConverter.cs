﻿using System;
using aPC.Common.Entities;
using amBXLib;

namespace aPC.Common.Integration
{
  public static class CompassDirectionConverter
  {
    public static Light GetLight(CompassDirection xiDirection, LightSection xiLights)
    {
      if (xiLights == null)
      {
        return null;
      }

      switch (xiDirection)
      {
        case CompassDirection.North:
          return xiLights.North;
        case CompassDirection.NorthEast:
          return xiLights.NorthEast;
        case CompassDirection.East:
          return xiLights.East;
        case CompassDirection.SouthEast:
          return xiLights.SouthEast;
        case CompassDirection.South:
          return xiLights.South;
        case CompassDirection.SouthWest:
          return xiLights.SouthWest;
        case CompassDirection.West:
          return xiLights.West;
        case CompassDirection.NorthWest:
          return xiLights.NorthWest;
        default:
          throw new InvalidOperationException("Unexpected Compass Direction: " + xiDirection);
      }
    }

    public static Fan GetFan(CompassDirection xiDirection, FanSection xiFans)
    {
      if (xiFans == null)
      {
        return null;
      }

      switch (xiDirection)
      {
        case CompassDirection.NorthEast:
        case CompassDirection.East:
          return xiFans.East;
        case CompassDirection.NorthWest:
        case CompassDirection.West:
          return xiFans.West;
        default:
          return null;
      }
    }

    public static CompassDirection GetDirection(eDirection xiDirection)
    {
      switch (xiDirection)
      {
        case eDirection.North:
          return CompassDirection.North;
        case eDirection.NorthEast:
          return CompassDirection.NorthEast;
        case eDirection.East:
          return CompassDirection.East;
        case eDirection.SouthEast:
          return CompassDirection.SouthEast;
        case eDirection.South:
          return CompassDirection.South;
        case eDirection.SouthWest:
          return CompassDirection.SouthWest;
        case eDirection.West:
          return CompassDirection.West;
        case eDirection.NorthWest:
          return CompassDirection.NorthWest;
        default:
          return CompassDirection.Everywhere;
      }
    }
  }
}
