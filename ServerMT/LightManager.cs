﻿using amBXLib;
using Common.Entities;
using Common.Integration;
using System.Linq;
using Common.Server.Managers;
using System;

namespace ServerMT
{
  class LightManager : ManagerBase<Light>
  {
    public LightManager(CompassDirection xiDirection) : base()
    {
      mDirection = xiDirection;
      base.SetupNewScene(mCurrentScene);
    }

    // A scene is applicable if there is at least one non-null light in the right direction defined.
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      if (xiNewScene.IsSynchronised)
      {
        return false;
      }

      var lLights = xiNewScene
        .Frames
        .Select(frame => frame.Lights)
        .Where(component => component != null)
        .Select(component => CompassDirectionConverter.GetLight(mDirection, component));

      return lLights.Any(light => light != null);
    }

    public override Data<Light> GetNext()
    {
      var lFrame = GetNextFrame();

      var lLength = lFrame.Length;
      int lFadeTime;
      Light lLight;

      if (lFrame.Lights == null)
      {
        lFadeTime = 0;
        lLight = null;
      }
      else
      {
        lFadeTime = lFrame.Lights.FadeTime;
        lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);
      }

      return new Data<Light>(lLight, lFadeTime, lLength);
    }

    CompassDirection mDirection;
  }
}
