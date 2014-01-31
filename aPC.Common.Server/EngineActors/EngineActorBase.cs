using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  //REVIEW: See FanManager re generics
  public abstract class ComponentActor<T> : EngineActorBase<ComponentData<T>> where T : Component
  {
    protected ComponentActor(EngineManager xiEngine, ComponentManager<T> xiManager) 
      : base(xiEngine, xiManager)
    {
    }
    
    protected abstract void Act(ComponentData<T> xiComponent);

    protected override void ActNextFrame()
    {
      var lRumbleData = Manager.GetNext();

      if (!lRumbleData.IsComponentNull)
      {
        Act(lRumbleData);
      }

      WaitforInterval(lRumbleData.Length);
    }
  }

  public abstract class EngineActorBase<T> where T : Data
  {
    protected EngineActorBase(EngineManager xiEngine, ManagerBase<T> xiManager)
    {
      Engine = xiEngine;
      Manager = xiManager;
    }

    public abstract eActorType ActorType();

    public void Run()
    {
      if (Manager.IsDormant)
      {
        WaitForDefaultInterval();
      }
      else
      {
        lock (Manager)
        {
          ActNextFrame();
          AdvanceScene();
        }
      }
    }

    protected abstract void ActNextFrame();

    protected void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    protected void WaitForDefaultInterval()
    {
      WaitforInterval(500);
    }

    protected void AdvanceScene()
    {
      Manager.AdvanceScene();
    }

    public void UpdateManager(amBXScene xiScene)
    {
      lock (Manager)
      {
        Manager.UpdateScene(xiScene);
      }
    }

    protected ManagerBase<T> Manager;
    protected EngineManager Engine;
  }
}
