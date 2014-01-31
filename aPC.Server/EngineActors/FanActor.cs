using aPC.Common;
using aPC.Common.Server.EngineActors;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using System;
using amBXLib;

namespace aPC.Server.EngineActors
{
  class FanActor : EngineActorBase<Fan>
  {
    public FanActor(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new FanManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    public override eActorType ActorType()
    {
      return eActorType.Fan;
    }

    protected override void Act(ComponentData<Fan> xiFanData)
    {
      Engine.UpdateFan(mDirection, xiFanData.Component);
    }

    private readonly CompassDirection mDirection;
  }
}
