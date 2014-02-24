﻿using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Common.Server.Conductors
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentConductor(ComponentActor<T> xiActor, ComponentHandler<T> xiHandler)
      : base(xiActor, xiHandler)
    {
    }

    public abstract eComponentType ComponentType();
  }
}
