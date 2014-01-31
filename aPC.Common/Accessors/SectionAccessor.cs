using System;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Common.Accessors
{
  public class SectionAccessor
  {
    //REVIEW: I wonder about the string description here. Particularly since there's currently just one magic value.
    // Exactly how to resolve it is complex - on a YAGNI basis one could argue for renaming this method GetSectionOff 
    // and omit the description parameter!
    public SectionBase GetSection(eSectionType xiFrametype, string xiDescription)
    {
      switch (xiFrametype)
      {
        case eSectionType.Light:
          return GetLightSection(xiDescription.ToLower());
        case eSectionType.Fan:
          return GetFanSection(xiDescription.ToLower());
        case eSectionType.Rumble:
          return GetRumbleSection(xiDescription.ToLower());
        default:
          throw new InvalidOperationException("Unexpected Frame type");
      }
    }

    private LightSection GetLightSection(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultLightSections.Off;
        default:
          throw new InvalidOperationException("Unexpected Light frame type");
      }
    }

    private FanSection GetFanSection(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultFanSections.Off;
        default:
          throw new InvalidOperationException("Unexpected Fan frame type");
      }
    }

    private RumbleSection GetRumbleSection(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultRumbleSections.Off;
        default:
          throw new InvalidOperationException("Unexpected Rumble frame type");
      }
    }

  }
}
