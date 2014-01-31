using System;
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

    [XmlAttribute]
    //REVIEW: You should use /// comments, and make these proper documentation rather than just notes for someone reading the source code
    // If IsEvent = true, this scene is for an "event":
    // * Ignore IsRepeatable booleans
    // * Once all frames have been run, return to the previously running Scene.
    public bool IsEvent;

    // Used in Server to decide on the set of Actor/s to use
    [XmlAttribute]
    public bool IsSynchronised;

    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames;

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

    #endregion
  }
}
