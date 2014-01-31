using aPC.Common.Accessors;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.Managers
{
  //REVIEW: See FanManager re generics
  public abstract class ManagerBase<T> where T : Data
  {
    protected ManagerBase()
    {
      var lScene = new SceneAccessor().GetScene("Empty");

      mPreviousScene = lScene;
      CurrentScene = lScene;
    }

    protected ManagerBase(Action xiEventCallback) : this()
    {
      mEventCallback = xiEventCallback;
    }

    public void UpdateScene(amBXScene xiNewScene)
    {
      lock (mSceneLock)
      {
        if (xiNewScene.IsEvent && CurrentScene.IsEvent)
        {
          // Skip updating the previous scene, to ensure that we don't get 
          // stuck in an infinite loop of events.
        }
        else
        {
          mPreviousScene = CurrentScene;
        }

        SetupNewScene(xiNewScene);
      }
    }

    protected void SetupNewScene(amBXScene xiNewScene)
    {
      if (FramesAreApplicable(xiNewScene.Frames))
      {
        CurrentScene = xiNewScene;
        SetupCurrentScene();
      }
      else if (FramesAreApplicable(mPreviousScene.Frames))
      {
        SetupCurrentScene();
      }
      else
      {
        IsDormant = true;
      }
    }

    private void SetupCurrentScene()
    {
      IsDormant = false;
      Ticker = new AtypicalFirstRunInfiniteTicker(CurrentScene.Frames.Count, CurrentScene.RepeatableFrames.Count);
    }

    protected abstract bool FramesAreApplicable(List<Frame> xiFrames);

    public abstract T GetNext();

    protected Frame GetNextFrame()
    {
      List<Frame> lFrames;

      lock (mSceneLock)
      {
        lFrames = Ticker.IsFirstRun
          ? CurrentScene.Frames
          : CurrentScene.RepeatableFrames;
      }

      if (!lFrames.Any())
      {
        // This can only happen in one of two cases:
        // * This isn't an event and all frames are not repeatable.
        // * There aren't any frames at all (though this should never happen)
        // Either way, return a frame which specifies everything off (as a failsafe)
        return new FrameAccessor().AllOff;
      }
      return lFrames[Ticker.Index];
    }

    public void AdvanceScene()
    {
      lock (mSceneLock)
      {
        Ticker.Advance();

        // If we've run the scene once through, we need to check for a few special circumstances
        if (Ticker.Index == 0)
        {
          DoSceneCompletedChecks();
        }
      }
    }

    private void DoSceneCompletedChecks()
    {
      if (CurrentScene.IsEvent)
      {
        // The event has completed one full cycle.  Revert to
        // previous scene
        SetupNewScene(mPreviousScene);
        if (mEventCallback != null)
        {
          mEventCallback();
        }
      }
      else if (!FramesAreApplicable(CurrentScene.RepeatableFrames))
      {
        IsDormant = true;
      }
    }

    public bool IsDormant;
    protected amBXScene CurrentScene;
    protected AtypicalFirstRunInfiniteTicker Ticker;

    private amBXScene mPreviousScene;
    private readonly object mSceneLock = new object();
    private readonly Action mEventCallback;
  }

  public abstract class ComponentManager<T> : ManagerBase<ComponentData<T>> where T : Component
  {
    protected ComponentManager(Action xiEventCallback)
      : base(xiEventCallback)
    {
    }
  }
}
