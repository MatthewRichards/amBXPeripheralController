using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using amBXLib;
using System;

namespace aPC.Common.Server.EngineActors
{
  //REVIEW: See FanManager re generics
  public class FrameActor : EngineActorBase<FrameData>
  {
    public FrameActor(EngineManager xiEngine) 
      : base (xiEngine, new FrameManager())
    {
    }

    public FrameActor(EngineManager xiEngine, Action xiEventComplete)
      : base(xiEngine, new FrameManager(xiEventComplete))
    {
    }

    public override eActorType ActorType()
    {
      return eActorType.Frame;
    }

    protected override void ActNextFrame()
    {
      var lFrameData = Manager.GetNext();
      var lFrame = lFrameData.Frame;

      if (lFrame.Lights != null)
      {
        UpdateLights(lFrame.Lights);
      }

      if (lFrame.Fans != null)
      {
        UpdateFans(lFrame.Fans);
      }

      if (lFrame.Rumbles != null)
      {
        UpdateRumbles(lFrame.Rumbles);
      }
      
      WaitforInterval(lFrame.Length);
    }

    private void UpdateLights(LightSection xiLights)
    {
      Engine.UpdateLight(CompassDirection.North, xiLights.North, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.NorthEast, xiLights.NorthEast, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.East, xiLights.East, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.SouthEast, xiLights.SouthEast, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.South, xiLights.South, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.SouthWest, xiLights.SouthWest, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.West, xiLights.West, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.NorthWest, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateFans(FanSection xiFans)
    {
      Engine.UpdateFan(CompassDirection.East, xiFans.East);
      Engine.UpdateFan(CompassDirection.West, xiFans.West);
    }

    private void UpdateRumbles(RumbleSection xiInputRumble)
    {
      Engine.UpdateRumble(CompassDirection.Everywhere, xiInputRumble.Rumble);
    }
  }
}
