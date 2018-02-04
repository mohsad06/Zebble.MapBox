﻿using System.Collections.Generic;

namespace Zebble.Plugin.MBox
{
    public class PolylineFeature : PolylineAnnotation, IFeature
    {
        public PolylineFeature()
        {
        }

        public PolylineFeature(PolylineAnnotation annotation)
        {
            Id = annotation.Id;
            Title = annotation.Title;
            SubTitle = annotation.SubTitle;
            Coordinates = annotation.Coordinates;
        }

        public Dictionary<string, object> Attributes { get; set; }
    }
}
