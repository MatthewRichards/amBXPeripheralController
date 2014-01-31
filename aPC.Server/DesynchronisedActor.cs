using amBXLib;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Server
{
  abstract class DesynchronisedActor
  {
    public abstract void Run();
    public abstract void UpdateManager(amBXScene xiScene);
  }

  class DesynchronisedActor<T> : DesynchronisedActor where T:Component
  {
    public DesynchronisedActor(CompassDirection xiDirection, EngineActorBase<T> xiActor)
    {
      mDirection = xiDirection;
      mActor = xiActor;
    }

    //REVIEW: Note that I had to add this to DesynchronisedActor because you can't expose the Actor
    // on the non-generically typed base class, and there's code that gets a list of mixed-type actors
    // and calls Run on each. But while this seems like a disadvantage, it's actually obeying a coding
    // guideline - Law of Demeter. I'd argue that DesynchronisedActor is more than just a data structure,
    // at least in concept, and hence you shouldn't call desync.Actor.Run() - you should call desync.Run()
    // and leave the internal implementation of that (i.e. delegate to the actor) up to this class.
    // At least, I think this is a reasonable claim :-) I've made the Actor private in consequence.
    public override void Run()
    {
      mActor.Run();
    }

    public override void UpdateManager(amBXScene xiScene)
    {
      mActor.UpdateManager(xiScene);
    }

    private readonly EngineActorBase<T> mActor;
    private readonly CompassDirection mDirection; //REVIEW: Not used?
  }
}
