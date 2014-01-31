using System;
using aPC.Common.Entities;
//REVIEW: amBXLib doesn't seem to be in git
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

      //REVIEW: You could probably do this using reflection, since your names are the same. I can't quite decide whether 
      // there are any major downsides to this approach, compared to yours. The biggest is probably potential 
      // inconsistency with the fan code. Maybe you could put attributes on the LightSection members indicating which 
      // CompassDirection they correspond to? Of course that leaks amBXLib into aPC.Common, and keeping it separate is
      // appealing. (Oh, actually it's used elsewhere. So that argument doesn't hold water).
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
  }
}
