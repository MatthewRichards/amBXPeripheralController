﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  [XmlRoot]
  [Serializable]
  public class amBXScene
  {
    // A complicated one.  The idea is that we want to mess with fans without touching the light (for example).
    // Saying you're exclusive means that we kill off everything and just use this.  Otherwise we'll merge in the 
    // changes specified by this.
    // TODO: not yet implemented
    [XmlAttribute]
    public bool IsExclusive;

    
    // If IsEvent = true, this scene is for an "event":
    // * Ignore IsRepeatable booleans
    // * Once all frames have been run, return to the previously running Scene.
    [XmlAttribute]
    public bool IsEvent;

    // Used in Server to decide on the set of Actor/s to use
    [XmlAttribute]
    public bool IsSynchronised;

    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames
    {
      get
      {
        return mFrames;
      }
      set
      {
        mFrames = value;
        // Clear the statistics
        mFrameStatistics = null;
      }
    }

    #region Helper Properties

    [XmlIgnore]
    public List<Frame> RepeatableFrames
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated)
                     .ToList();
      }
    }

    [XmlIgnore]
    public FrameStatistics FrameStatistics
    {
      get
      {
        if (mFrameStatistics == null)
        {
          mFrameStatistics = new FrameStatistics(Frames);
        }
        return mFrameStatistics;
      }
    }

    #endregion

    [XmlIgnore]
    private List<Frame> mFrames;

    [XmlIgnore]
    private FrameStatistics mFrameStatistics;
  }
}
