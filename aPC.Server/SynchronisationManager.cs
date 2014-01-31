using System;

namespace aPC.Server
{
  class SynchronisationManager
  {
    public SynchronisationManager()
    {
      IsSynchronised = true;
    }

    public void RunWhileSynchronised(Action xiAction)
    {
      while (IsSynchronised)
      {
        xiAction();
      }
    }

    public void RunWhileUnSynchronised(Action xiAction)
    {
      while (!IsSynchronised)
      {
        xiAction();
      }
    }

    public bool IsSynchronised
    {
      get
      {
        lock (mLocker)
        {
          return mIsSynchronised;
        }
      }
      set
      {
        lock (mLocker)
        {
          mWasPreviouslySynchronised = mIsSynchronised;
          mIsSynchronised = value;
        }
      }
    }

    public bool WasPreviouslySynchronised
    {
      get
      {
        return mWasPreviouslySynchronised;
      }
    }

    private readonly object mLocker = new object();

    private bool mIsSynchronised;
    private bool mWasPreviouslySynchronised;
  }
}
