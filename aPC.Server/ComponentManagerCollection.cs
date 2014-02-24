﻿using aPC.Server.EngineActors;
using aPC.Server.Conductors;
using aPC.Common.Server;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using aPC.Server.SceneHandlers;

namespace aPC.Server
{
  class ComponentManagerCollection
  {
    public ComponentManagerCollection(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupManagers(xiEngine, xiEventComplete);
    }

    private void SetupManagers(EngineManager xiEngine, Action xiAction)
    {
      mLightManagers = new List<LightConductor>();
      mFanManagers = new List<FanConductor>();
      mRumbleManagers = new List<RumbleConductor>();

      mLightManagers.Add(new LightConductor(new LightActor(eDirection.North, xiEngine), new LightHandler(eDirection.North, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.NorthEast, xiEngine), new LightHandler(eDirection.NorthEast, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.East, xiEngine), new LightHandler(eDirection.East, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.SouthEast, xiEngine), new LightHandler(eDirection.SouthEast, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.South, xiEngine), new LightHandler(eDirection.South, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.SouthWest, xiEngine), new LightHandler(eDirection.SouthWest, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.West, xiEngine), new LightHandler(eDirection.West, xiAction)));
      mLightManagers.Add(new LightConductor(new LightActor(eDirection.NorthWest, xiEngine), new LightHandler(eDirection.NorthWest, xiAction)));

      mFanManagers.Add(new FanConductor(new FanActor(eDirection.East, xiEngine), new FanHandler(eDirection.East, xiAction)));
      mFanManagers.Add(new FanConductor(new FanActor(eDirection.West, xiEngine), new FanHandler(eDirection.West, xiAction)));

      mRumbleManagers.Add(new RumbleConductor(new RumbleActor(eDirection.Center, xiEngine), new RumbleHandler(eDirection.Center, xiAction)));
    }

    public void RunAllManagersDeSynchronised(SynchronisationManager xiSyncManager)
    {
      bool lLighstsFinished = RunTaskOnLights(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));
      bool lFansFinished = RunTaskOnFans(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));
      bool lRumblesFinished = RunTaskOnRumbles(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));

      while (!(lLighstsFinished && lFansFinished && lRumblesFinished))
      {
        Thread.Sleep(1000);
      }
    }

    public void UpdateAllManagers(amBXScene xiScene)
    {
      bool lLighstsFinished = RunTaskOnLights(conductor => conductor.UpdateScene(xiScene));
      bool lFansFinished = RunTaskOnFans(conductor => conductor.UpdateScene(xiScene));
      bool lRumblesFinished = RunTaskOnRumbles(conductor => conductor.UpdateScene(xiScene));

      while (!(lLighstsFinished && lFansFinished && lRumblesFinished))
      {
        Thread.Sleep(1000);
      }
    }

    private bool RunTaskOnLights(Action<LightConductor> xiAction)
    {
      Parallel.ForEach(mLightManagers, xiAction);
      return true;
    }

    private bool RunTaskOnFans(Action<FanConductor> xiAction)
    {
      Parallel.ForEach(mFanManagers, xiAction);
      return true;
    }

    private bool RunTaskOnRumbles(Action<RumbleConductor> xiAction)
    {
      Parallel.ForEach(mRumbleManagers, xiAction);
      return true;
    }

    private List<LightConductor> mLightManagers;
    private List<FanConductor> mFanManagers;
    private List<RumbleConductor> mRumbleManagers;
  }
}
