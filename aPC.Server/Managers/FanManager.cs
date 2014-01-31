using amBXLib;
using aPC.Common.Entities;
using aPC.Common.Integration;
using System.Linq;
using System;
using aPC.Common.Server.Managers;
using System.Collections.Generic;

namespace aPC.Server.Managers
{
  //REVIEW: Ok, so now I'm just showing off.. However I got a bit upset over the whole GetNext vs GetNextFrame thing.
  // I still think it's confusing having those different names, but at least now you don't need any casts in FanActor and 
  // it's perhaps a little more obvious what's going on :-)
  class FanManager : ComponentManager<Fan>
  {
    public FanManager(CompassDirection xiDirection)
      : this(xiDirection, null)
    {
    }

    public FanManager(CompassDirection xiDirection, Action xiEventCallback)
      : base(xiEventCallback)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null fan in a "somewhat" correct direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFans = xiFrames
        .Select(frame => frame.Fans)
        .Where(section => section != null)
        .Select(section => CompassDirectionConverter.GetFan(mDirection, section));

      return lFans.Any(fan => fan != null);
    }

    public override ComponentData<Fan> GetNext()
    {
      var lFrame = GetNextFrame();
      var lFan = CompassDirectionConverter.GetFan(mDirection, lFrame.Fans);

      return lFan == null
        ? new ComponentData<Fan>(lFrame.Length)
        : new ComponentData<Fan>(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    readonly CompassDirection mDirection;
  }
}
