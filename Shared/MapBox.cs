﻿namespace Zebble.Plugin
{
    using System;
    using System.Collections.Generic;
    using Framework;

    public partial class MapBox
    {
        Services.GeoLocation center = new Services.GeoLocation(51.5, 0.12);
        float zoom = 13;
        bool showsUserLocation = true;
        List<Map.Annotation> annotations = new List<Map.Annotation>();

        internal readonly AsyncEvent ApiZoomChanged = new AsyncEvent();
        internal readonly AsyncEvent ApiCenterChanged = new AsyncEvent();
        internal event Action<Map.Annotation> AnnotationAdded, AnnotationRemoved;
        public event Action<Map.Annotation> AnnotationClicked;

        public MapBox()
        {
            AccessToken = Config.Get("MapBox.Access.Token");
        }

        public string AccessToken { get; set; }
        public string StyleUrl { get; set; }
        public string AnnotationImagePath;
        public Size AnnotationImageSize = new Size(16, 16);

        public IEnumerable<Map.Annotation> Annotations => annotations;

        public Services.GeoLocation Center
        {
            get { return center; }
            set
            {
                if (center == value || value == null) return;
                center = value;
                ApiCenterChanged.Raise();
            }
        }

        public bool ShowsUserLocation
        {
            get { return showsUserLocation; }
            set
            {
                if (showsUserLocation == value) return;
                showsUserLocation = value;
            }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                if (zoom.AlmostEquals(value)) return;
                zoom = value;
                ApiZoomChanged.Raise();
            }
        }

        public void Add(params Map.Annotation[] annotations)
        {
            this.annotations.AddRange(annotations);
            foreach (var a in annotations) AnnotationAdded?.Invoke(a);
        }

        public void Remove(params Map.Annotation[] annotations)
        {
            this.annotations.Remove(annotations);
            foreach (var a in annotations) AnnotationRemoved?.Invoke(a);
        }
    }
}