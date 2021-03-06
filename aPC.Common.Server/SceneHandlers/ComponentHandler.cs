﻿using System;
using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.SceneHandlers
{
  public abstract class ComponentHandler<T> : SceneHandlerBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentHandler(amBXScene xiScene, Action xiEventComplete)
      : base(xiScene, xiEventComplete)
    {
    }
  }
}
