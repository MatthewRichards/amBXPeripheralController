using aPC.Common.Entities;
using aPC.Common.Integration;
using System;
using amBXLib;
using System.Collections.Generic;
using System.Threading;

namespace aPC.Common.Server.Managers
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  //REVIEW: Possibly does too many things? I can imagine a class that just does the setup and exposes 
  // the resulting lights etc for someone else to update. Actually I've used the word "etc" just like
  // you did, which suggests perhaps that's one class for updating lights, one for fans, ... 
  // Getting too fine-grained? Dunno.
  public class EngineManager : IDisposable
  {
    public EngineManager()
    {
      mEngine = new amBX(1, 0, "amBXNotification", "1.0");
      mLights = new Dictionary<CompassDirection, amBXLight>();
      mFans = new Dictionary<CompassDirection, amBXFan>();
      mRumbles = new Dictionary<CompassDirection, amBXRumble>();
      InitialiseEngine();
    }

    #region Engine Setup

    private void InitialiseEngine()
    {
      CreateLight(CompassDirection.North);
      CreateLight(CompassDirection.NorthEast);
      CreateLight(CompassDirection.East);
      CreateLight(CompassDirection.SouthEast);
      CreateLight(CompassDirection.South);
      CreateLight(CompassDirection.SouthWest);
      CreateLight(CompassDirection.West);
      CreateLight(CompassDirection.NorthWest);

      CreateFan(CompassDirection.East);
      CreateFan(CompassDirection.West);

      CreateRumble(CompassDirection.Everywhere);
    }

    private void CreateLight(CompassDirection xiDirection)
    {
      var lLight = mEngine.CreateLight(xiDirection, RelativeHeight.AnyHeight);
      mLights.Add(xiDirection, lLight);
    }

    private void CreateFan(CompassDirection xiDirection)
    {
      var lFan = mEngine.CreateFan(xiDirection, RelativeHeight.AnyHeight);
      mFans.Add(xiDirection, lFan);
    }

    private void CreateRumble(CompassDirection xiDirection)
    {
      var lRumble = mEngine.CreateRumble(xiDirection, RelativeHeight.AnyHeight);
      mRumbles.Add(xiDirection, lRumble);
    }

    #endregion

    #region Updating

    public void UpdateLight(CompassDirection xiDirection, Light xiInputLight, int xiFadeTime)
    {
      ThreadPool.QueueUserWorkItem(_ => UpdateLightInternal(mLights[xiDirection], xiInputLight, xiFadeTime));
    }

    private void UpdateLightInternal(amBXLight xiLight, Light xiInputLight, int xiFadeTime)
    {
      if (xiInputLight == null)
      {
        // No change - don't touch!
        return;
      }
      //REVIEW: Where's Intensity gone?
      xiLight.Color = new amBXColor { Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue };
      xiLight.FadeTime = xiFadeTime;
    }

    public void UpdateFan(CompassDirection xiDirection, Fan xiInputFan)
    {
      UpdateFanInternal(mFans[xiDirection], xiInputFan);
    }

    private void UpdateFanInternal(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    public void UpdateRumble(CompassDirection xiDirection, Rumble xiInputRumble)
    {
      UpdateRumbleInternal(mRumbles[xiDirection], xiInputRumble);
    }

    protected void UpdateRumbleInternal(amBXRumble xiRumble, Rumble xiInputRumble)
    {
      if (xiInputRumble == null)
      {
        return;
      }

      RumbleType lRumbleType;

      try
      {
        //REVIEW: Could have a TryGetRumbleType and avoid the exception handling and other boilerplate
        lRumbleType = RumbleTypeConverter.GetRumbleType(xiInputRumble.RumbleType);
      }
      catch (InvalidRumbleException)
      {
        return;
      }

      xiRumble.RumbleSetting = new amBXRumbleSetting
      {
        Intensity = xiInputRumble.Intensity,
        Speed = xiInputRumble.Speed,
        Type = lRumbleType
      };
    }

    #endregion

    public void Dispose()
    {
      mEngine.Dispose();
    }

    private readonly amBX mEngine;

    private readonly Dictionary<CompassDirection, amBXLight> mLights;
    private readonly Dictionary<CompassDirection, amBXFan> mFans;
    private readonly Dictionary<CompassDirection, amBXRumble> mRumbles;
  }
}
