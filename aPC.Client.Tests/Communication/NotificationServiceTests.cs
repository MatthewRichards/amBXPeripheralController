﻿using System;
using NUnit.Framework;
using aPC.Client.Communication;
using aPC.Common.Communication;
using aPC.Common.Communication.Tests;

namespace aPC.Client.Tests.Communication
{
  [TestFixture]
  class NotificationServiceTests
  {
    [TestFixtureSetUp]
    public void SetupTests()
    {
      var lUrl = CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost")
        .Replace("amBXPeripheralController", "aPCTest");

      mHost = new TestNotificationService(lUrl);
      mClient = new NotificationClient(lUrl);
    }

    [SetUp]
    public void ClearHost()
    {
      mHost.ClearScenes();
    }

    [TestFixtureTearDown]
    public void TearDownTests()
    {
      mHost.Dispose();
    }

    [Test]
    public void PushingACustomScene_SendsTheExpectedScene()
    {
      mClient.PushCustomScene("ArbitraryScene");

      Assert.AreEqual(1, mHost.Scenes.Count);
      Assert.AreEqual(false, mHost.Scenes[0].Item1);
      Assert.AreEqual("ArbitraryScene", mHost.Scenes[0].Item2);
    }

    [Test]
    public void PushingAnIntegratedScene_SendsTheExpectedScene()
    {
      mClient.PushIntegratedScene("IntegratedScene");

      Assert.AreEqual(1, mHost.Scenes.Count);
      Assert.AreEqual(true, mHost.Scenes[0].Item1);
      Assert.AreEqual("IntegratedScene", mHost.Scenes[0].Item2);
    }

    private NotificationClient mClient;
    private TestNotificationService mHost;
  }
}
