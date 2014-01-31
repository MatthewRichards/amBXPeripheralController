using amBXLib;
using aPC.Common.Server.EngineActors;
using aPC.Server.EngineActors;
using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Server
{
  class DesynchronisedActorManager
  {
    public DesynchronisedActorManager(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupActors(xiEngine, xiEventComplete);
    }

    private void SetupActors(EngineManager xiEngine, Action xiAction)
    {
      mDesynchronisedActors = new List<DesynchronisedActor>();

      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.North,     new LightActor(CompassDirection.North, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.NorthEast, new LightActor(CompassDirection.NorthEast, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.East,      new LightActor(CompassDirection.East, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.SouthEast, new LightActor(CompassDirection.SouthEast, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.South,     new LightActor(CompassDirection.South, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.SouthWest, new LightActor(CompassDirection.SouthWest, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.West,      new LightActor(CompassDirection.West, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.NorthWest, new LightActor(CompassDirection.NorthWest, xiEngine, xiAction)));

      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.East, new FanActor(CompassDirection.East, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.West, new FanActor(CompassDirection.West, xiEngine, xiAction)));

      mDesynchronisedActors.Add(new DesynchronisedActor(CompassDirection.Everywhere, new RumbleActor(CompassDirection.Everywhere, xiEngine, xiAction)));
    }

    //REVIEW: I couldn't find any code using this method! But I reckon the new version would be easier to use...
    public List<DesynchronisedActor<T>> ActorsWithType<T>() where T:Component
    {
      return mDesynchronisedActors
        .OfType<DesynchronisedActor<T>>()
        .ToList();
    }

    public List<DesynchronisedActor> AllActors()
    {
      return mDesynchronisedActors;
    }

    private List<DesynchronisedActor> mDesynchronisedActors;
  }
}
