using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server.EngineActors
{
  class RumbleActor : EngineActorBase<Rumble>
  {
    public RumbleActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new RumbleManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }
    
    public override eActorType ActorType()
    {
      return eActorType.Rumble;
    }

    //REVIEW: Another benefit of having added those generics
    // I'd prefer to pass in just the rumble, but LightActor uses the fade time too. Perhaps could pass that in as well? 
    // Not sure - don't want multiple arguments if we can help it!
    protected override void Act(ComponentData<Rumble> xiRumbleData)
    {
      Engine.UpdateRumble(mDirection, xiRumbleData.Component);
    }

    private readonly CompassDirection mDirection;
  }
}
