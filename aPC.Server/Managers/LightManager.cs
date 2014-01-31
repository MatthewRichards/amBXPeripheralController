using amBXLib;
using aPC.Common.Entities;
using aPC.Common.Integration;
using System.Linq;
using aPC.Common.Server.Managers;
using System;
using System.Collections.Generic;

namespace aPC.Server.Managers
{
  //REVIEW: See FanManager re generics
  class LightManager : ComponentManager<Light>
  {
    public LightManager(CompassDirection xiDirection) 
      : this(xiDirection, null)
    {
    }

    public LightManager(CompassDirection xiDirection, Action xiEventCallback) 
      : base(xiEventCallback)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lLights = xiFrames
        .Select(frame => frame.Lights)
        .Where(section => section != null)
        .Select(section => CompassDirectionConverter.GetLight(mDirection, section));

      return lLights.Any(light => light != null);
    }

    public override ComponentData<Light> GetNext()
    {
      // Temporary debug trace
      Console.WriteLine(mDirection + " - GetNext     - " + DateTime.Now.Ticks);

      var lFrame = GetNextFrame();

      int lFadeTime;
      var lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);

      return lLight == null
        ? new ComponentData<Light>(lFrame.Length)
        : new ComponentData<Light>(lLight, lFrame.Lights.FadeTime, lFrame.Length);
    }

    readonly CompassDirection mDirection;
  }
}
