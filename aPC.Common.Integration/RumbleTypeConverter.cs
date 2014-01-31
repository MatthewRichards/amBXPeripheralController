using amBXLib;

namespace aPC.Common.Integration
{
  public static class RumbleTypeConverter
  {
    //REVIEW: I like having an enum for the rumble types. So it feels a bit of a shame that you use strings
    // everywhere and just convert them at this point.
    // I'd also rather this was done without a big switch-block, using some manner of reflection.
    public static RumbleType GetRumbleType(string xiRumbleType)
    {
      switch (xiRumbleType.ToLower())
      {
        case "boing":
          return RumbleType.Boing;
        case "crash":
          return RumbleType.Crash;
        case "engine":
          return RumbleType.Engine;
        case "explosion":
          return RumbleType.Explosion;
        case "hit":
          return RumbleType.Hit;
        case "quake":
          return RumbleType.Quake;
        case "rattle":
          return RumbleType.Rattle;
        case "road":
          return RumbleType.Road;
        case "shot":
          return RumbleType.Shot;
        case "thud":
          return RumbleType.Thud;
        case "thunder":
          return RumbleType.Thunder;
      }

      throw new InvalidRumbleException();
    }
  }
}
