using aPC.Common.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace aPC.Common.Server.Managers
{
  //REVIEW: See FanManager re generics
  public class FrameManager : ManagerBase<FrameData>
  {
    public FrameManager() 
      : this(null)
    {
    }

    public FrameManager(Action xiEventComplete)
      : base(xiEventComplete)
    {
      SetupNewScene(CurrentScene);
    }

    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFrames = xiFrames
        .Where(frame => frame.Lights != null || 
                        frame.Fans   != null || 
                        frame.Rumbles != null);

      return lFrames.Any(frame => frame != null);
    }

    public override FrameData GetNext()
    {
      var lFrame = GetNextFrame();
      //REVIEW: FrameData should just take a Frame maybe?
      return new FrameData(lFrame, 0, lFrame.Length);
    }
  }
}
