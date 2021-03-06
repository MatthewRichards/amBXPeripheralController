﻿using aPC.Client.Communication;
using aPC.Common.Communication;

namespace aPC.Client
{
  public class ClientTask
  {
    public ClientTask(Settings xiSettings, INotificationClient xiNotificationClient)
    {
      mNotificationClient = xiNotificationClient;
      mSettings = xiSettings;
    }

    public void Push()
    {
      if (mSettings.IsIntegratedScene)
      {
        mNotificationClient.PushIntegratedScene(mSettings.SceneData);
      }
      else
      {
        mNotificationClient.PushCustomScene(mSettings.SceneData);
      }
    }

    private readonly Settings mSettings;
    private readonly INotificationClient mNotificationClient;
  }
}
