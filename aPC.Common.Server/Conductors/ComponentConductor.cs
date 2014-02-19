﻿using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using aPC.Common.Server.SceneHandlers;
using System;

namespace aPC.Common.Server.Conductors
{
  public abstract class ComponentConductor<T> : ConductorBase<ComponentSnapshot<T>> where T : IComponent
  {
    public ComponentConductor(eDirection xiDirection, ComponentActor<T> xiActor, ComponentHandler<T> xiHandler)
      : base(xiActor, xiHandler)
    {
      Direction = xiDirection;
    }

    public abstract eComponentType ComponentType();
    protected eDirection Direction;
  }
}