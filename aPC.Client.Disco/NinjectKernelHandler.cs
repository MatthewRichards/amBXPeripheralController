﻿using aPC.Common.Client;
using aPC.Common.Communication;
using aPC.Client.Disco.Communication;
using aPC.Client.Disco.Generators;
using aPC.Common.Entities;

namespace aPC.Client.Disco
{
  class NinjectKernelHandler : NinjectKernelHandlerBase
  {
    public NinjectKernelHandler(Settings xiSettings)
    {
      SetupSettingsBinding(xiSettings);
    }

    protected override void SetupBindings()
    {
      mKernel.Bind<INotificationClient>().To<NotificationClient>();
      mKernel.Bind<IGenerator<amBXScene>>().To<RandomSceneGenerator>();
      mKernel.Bind<IGenerator<LightSection>>().To<RandomLightSectionGenerator>();
    }

    private void SetupSettingsBinding(Settings xiSettings)
    {
      mKernel.Bind<Settings>().ToConstant(xiSettings);
    }
  }
}
