﻿using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  public class Fan : IComponent
  {
    [XmlAttribute]
    public float Intensity;

    public eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}