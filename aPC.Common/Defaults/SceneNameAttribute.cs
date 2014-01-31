using System;

namespace aPC.Common.Defaults
{
  //REVIEW: See notes in SceneAccessor
  [AttributeUsage(AttributeTargets.Property)]
  internal class SceneNameAttribute : Attribute
  {
    public string Name { get; private set; }

    public SceneNameAttribute(string name)
    {
      Name = name;
    }
  }
}