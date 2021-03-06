﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Common.Entities;
using aPC.Common.Builders;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  class SceneHandlerBaseTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var lInitialSceneFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(false)
        .WithFrameLength(1000)
        .WithLightSection(Defaults.DefaultLightSections.SoftPink)
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(2000)
        .WithLightSection(Defaults.DefaultLightSections.Orange)
        .Build();

      var lBlueEventFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(500)
        .WithLightSection(Defaults.DefaultLightSections.JiraBlue)
        .Build();

      var lPurpleEventFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(250)
        .WithLightSection(Defaults.DefaultLightSections.StrongPurple)
        .Build();

      var lUnrepeatedFrames = new FrameBuilder()
      .AddFrame()
        .WithRepeated(false)
        .WithFrameLength(125)
        .WithLightSection(Defaults.DefaultLightSections.Red)
        .Build();

      mInitialScene = new amBXScene
      {
        IsEvent = false,
        IsSynchronised = false,
        IsExclusive = false,
        Frames = lInitialSceneFrames
      };

      mBlueEvent = new amBXScene
      {
        IsEvent = true,
        IsSynchronised = true,
        IsExclusive = false,
        Frames = lBlueEventFrames
      };

      mPurpleEvent = new amBXScene
      {
        IsEvent = true,
        IsSynchronised = true,
        IsExclusive = false,
        Frames = lPurpleEventFrames
      };

      mUnrepeatedScene = new amBXScene
      {
        IsEvent = false,
        IsSynchronised = false,
        IsExclusive = false,
        Frames = lUnrepeatedFrames
      };
    }

    [Test]
    public void NewSceneHandler_IsNotDormant()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      Assert.IsFalse(lHandler.IsDormant);
    }

    [Test]
    public void GetNextFrame_GetsExpectedData()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      
      var lFrame = lHandler.NextFrame;
      var lExpectedFrame = mInitialScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lFrame.Lights);
    }

    [Test]
    public void AdvancingHandlerOnFirstRun_GivesSecondFrame()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      
      lHandler.AdvanceScene();
      var lFrame = lHandler.NextFrame;
      var lExpectedFrame = mInitialScene.Frames[1];

      Assert.AreEqual(lExpectedFrame.Length, lFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lFrame.Lights);
    }

    [Test]
    public void AdvancingHandlerThreeTimes_ReturnsToFirstRepeatableFrame()
    {
      var lHandler = new TestSceneHandler(mInitialScene);

      lHandler.AdvanceScene();
      lHandler.AdvanceScene();

      var lFrame = lHandler.NextFrame;
      // The first repeatable frame is the second one in the scene
      var lExpectedFrame = mInitialScene.Frames[1];

      Assert.AreEqual(lExpectedFrame.Length, lFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lFrame.Lights);
    }

    [Test]
    public void AfterRunningThroughAnEvent_RevertsToPreviousScene()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      lHandler.UpdateScene(mBlueEvent);

      lHandler.AdvanceScene();
      var lFrame = lHandler.NextFrame;
      var lExpectedFrame = mInitialScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lFrame.Lights);
    }

    [Test]
    public void AfterRunningThroughAnEvent_WithASpecifiedAction_GivenActionIsRun()
    {
      var lActionRun = false;
      var lHandler = new TestSceneHandler(mInitialScene, () => lActionRun = true);
      
      lHandler.UpdateScene(mBlueEvent);
      lHandler.AdvanceScene();

      Assert.IsTrue(lActionRun);
    }

    [Test]
    public void NoMoreApplicableScenes_MarksHandlerAsDormant()
    {
      var lHandler = new TestSceneHandler(mUnrepeatedScene);
      lHandler.AdvanceScene();

      var lFrame = lHandler.NextFrame;

      Assert.IsTrue(lHandler.IsDormant);
      Assert.AreEqual(default(int), lFrame.Length);
      Assert.AreEqual(default(bool), lFrame.IsRepeated);
      Assert.IsNull(lFrame.Lights);
    }

    [Test]
    public void HandlerWithEvent_PushingAnotherEvent_DoesNotUpdatePreviousScene()
    {
      var lHandler = new TestSceneHandler(mInitialScene);
      lHandler.UpdateScene(mBlueEvent);
      lHandler.UpdateScene(mPurpleEvent);

      // Confirm it's actually updated the Scene Handler
      var lEventFrame = lHandler.NextFrame;
      Assert.AreEqual(lEventFrame.Lights, mPurpleEvent.Frames.Single().Lights);

      lHandler.AdvanceScene();

      // Confirm the previous scene is mInitialScene
      var lPreviousFrame = lHandler.NextFrame;
      var lExpectedFrame = mInitialScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lPreviousFrame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lPreviousFrame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lPreviousFrame.Lights);
    }

    private amBXScene mInitialScene;
    private amBXScene mBlueEvent;
    private amBXScene mPurpleEvent;
    private amBXScene mUnrepeatedScene;
  }
}
