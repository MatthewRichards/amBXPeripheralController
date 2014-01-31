using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server.EngineActors
{
  class LightActor : EngineActorBase<Light>
  {
    public LightActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new LightManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    public override eActorType ActorType()
    {
      return eActorType.Light;
    }

    protected override void Act(ComponentData<Light> xiLightData)
    {
      // Temporary Debug trace:
      Console.WriteLine(mDirection + " - UpdateLight - " + DateTime.Now.Ticks);
      Engine.UpdateLight(mDirection, xiLightData.Component, xiLightData.FadeTime);
    }

    private readonly CompassDirection mDirection;
  }
}
