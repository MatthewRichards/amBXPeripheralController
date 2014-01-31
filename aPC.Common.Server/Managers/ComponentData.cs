using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  //REVIEW: See FanManager re generics
  public class ComponentData<T> : Data where T : Component
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentData(int xiLength) : this(null, 0, xiLength)
    {
    }

    public ComponentData(T xiItem, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Component = xiItem;
    }

    public bool IsComponentNull
    {
      get
      {
        return Component == null;
      }
    }

    public T Component;
  }
}
