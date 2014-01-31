using aPC.Common;
using aPC.Common.Accessors;
using aPC.Common.Entities;
using aPC.Common.Server.Communication;
using aPC.Common.Server.Managers;
using aPC.Common.Server.EngineActors;
using aPC.Server.Communication;
using System.Threading.Tasks;

namespace aPC.Server
{
  class ServerTask
  {
    public ServerTask()
    {
      mSyncManager = new SynchronisationManager();
    }

    internal void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        mFrame = new FrameActor(lEngine, EventComplete);
        mDesynchronisedManager = new DesynchronisedActorManager(lEngine, EventComplete);

        // Start the default initial scene
        Update(new SceneAccessor().GetScene("Default_RedVsBlue"));

        while (true)
        {
          if (mSyncManager.IsSynchronised)
          {
            mSyncManager.RunWhileSynchronised(mFrame.Run);
          }
          else
          {
            Parallel.ForEach(mDesynchronisedManager.AllActors(), 
                             actor => mSyncManager.RunWhileUnSynchronised(actor.Run));
          }
        }
      }

      //REVIEW: It feels like *some* way to stop this code would be nice. But maybe not :-)
    }

    internal void Update(amBXScene xiScene)
    {
      if (xiScene.IsSynchronised)
      {
        mSyncManager.IsSynchronised = true;
      }
      else
      {
        mSyncManager.IsSynchronised = false;
      }

      UpdateActors(xiScene);
    }

    private void UpdateActors(amBXScene xiScene)
    {
      if (!xiScene.IsEvent)
      {
        UpdateSynchronisedActor(xiScene);
        UpdateUnsynchronisedActors(xiScene);
      }
      else 
      {
        mIsCurrentlyRunningEvent = true;

        if (mSyncManager.IsSynchronised)
        {
          UpdateSynchronisedActor(xiScene);
        }
        else
        {
          UpdateUnsynchronisedActors(xiScene);
        }
      }
    }

    private void UpdateSynchronisedActor(amBXScene xiScene)
    {
      mFrame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedActors(amBXScene xiScene)
    {
      Parallel.ForEach(mDesynchronisedManager.AllActors(),
                       actor => actor.UpdateManager(xiScene));
    }

    /// <remarks>
    ///   This is called when an event has been completed and reinstates the previous synchronicity before the event 
    ///   was run.  In the end, this is only important when the event causes a change in synchronicity 
    ///   (e.g. from desynchronised to synchronised).
    ///   Note that there is a potential issue here: if we ran a Desynchronised event we will call this method 
    ///   multiple times (once per light \ fan etc.).  mIsCurrentlyRunningEvent should however stop this 
    ///   being a problem?
    /// </remarks>
    internal void EventComplete()
    {
      if (mIsCurrentlyRunningEvent && mSyncManager.WasPreviouslySynchronised != mSyncManager.IsSynchronised)
      {
        mIsCurrentlyRunningEvent = false;
        mSyncManager.IsSynchronised = !mSyncManager.IsSynchronised;
      }
    }

    private FrameActor mFrame;
    private DesynchronisedActorManager mDesynchronisedManager;

    private readonly SynchronisationManager mSyncManager;
    private bool mIsCurrentlyRunningEvent;
  }
}