﻿using aPC.Common.Entities;
using System;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder : SectionBuilderBase<Light>
  {
    public LightSectionBuilder()
    {
      mLightSection = new LightSection();
      mLightSpecified = false;
    }


    public LightSectionBuilder WithFadeTime(int xiFadeTime)
    {
      SetFadeTime(mLightSection, xiFadeTime);
      return this;
    }

    public LightSectionBuilder WithAllLights(Light xiLight)
    {
      WithLightInDirection(eDirection.North, xiLight);
      WithLightInDirection(eDirection.NorthEast, xiLight);
      WithLightInDirection(eDirection.East, xiLight);
      WithLightInDirection(eDirection.SouthEast, xiLight);
      WithLightInDirection(eDirection.South, xiLight);
      WithLightInDirection(eDirection.SouthWest, xiLight);
      WithLightInDirection(eDirection.West, xiLight);
      WithLightInDirection(eDirection.NorthWest, xiLight);

      return this;
    }

    public LightSectionBuilder WithLightInDirection(eDirection xiDirection, Light xiLight)
    {
      var lLightExists = mLightSection.SetComponentValueInDirection(xiLight, xiDirection);
      if (!lLightExists)
      {
        throw new InvalidOperationException("Attempted to update a light which does not exist");
      }
      mLightSpecified = true;
      return this;
    }

    public LightSectionBuilder WithLightInDirectionIfPhysical(eDirection xiDirection, Light xiLight)
    {
      var lLightExists = mLightSection.SetPhysicalComponentValueInDirection(xiLight, xiDirection);
      if (lLightExists)
      {
        mLightSpecified = true;
      }
      return this;
    }

    public LightSection Build()
    {
      if (!LightSectionIsValid)
      {
        throw new ArgumentException("Incomplete LightSection built.  At least one light and the Fade Time must be specified.");
      }

      return mLightSection;
    }

    private bool LightSectionIsValid
    {
      get
      {
        return mLightSection.FadeTime != default(int) && mLightSpecified;
      }
    }

    private LightSection mLightSection;
    private bool mLightSpecified;
  }
}
