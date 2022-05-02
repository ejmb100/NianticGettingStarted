// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System.Collections.Generic;

namespace Niantic.ARDK.AR.Anchors
{
  internal class _SerializableAnchorsByType
  {
    public _SerializableAnchorsByType(List<_SerializableARAnchor> baseAnchors, List<_SerializableARPlaneAnchor> planeAnchors, List<_SerializableARImageAnchor> imageAnchors)
    {
      BaseAnchors = baseAnchors;
      PlaneAnchors = planeAnchors;
      ImageAnchors = imageAnchors;
    }
    
    public List<_SerializableARAnchor> BaseAnchors { get; }
    public List<_SerializableARPlaneAnchor> PlaneAnchors { get; }
    public List<_SerializableARImageAnchor> ImageAnchors { get; }
  }
}
