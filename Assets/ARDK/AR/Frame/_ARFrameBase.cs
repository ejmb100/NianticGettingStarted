// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;
using System.Linq;

using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.Awareness;
using Niantic.ARDK.AR.Awareness.Depth;
using Niantic.ARDK.AR.Awareness.Semantics;
using Niantic.ARDK.AR.Camera;
using Niantic.ARDK.AR.Image;
using Niantic.ARDK.AR.PointCloud;
using Niantic.ARDK.AR.SLAM;
using Niantic.ARDK.Utilities.Collections;

using UnityEngine;

namespace Niantic.ARDK.AR.Frame
{
  [Serializable]
  internal abstract class _ARFrameBase
  {
    public IDepthPointCloud DepthPointCloud { get; internal set; }

    // TODO AR-8359 Remove this method for ARDK 2.0
    internal IARFrame _Serialize
    (
      IARFrame source, 
      bool includeImageBuffers = true, 
      bool includeAwarenessBuffers = true, 
      int compressionLevel = 70, 
      bool includeFeaturePoints = false
    )
    {
      var serializedFrame = _SerializeWithoutBuffers(source);
      if (includeImageBuffers)
      {
        _SerializableImageBuffer serializedImageBuffer = null;
    
        var imageBuffer = source.CapturedImageBuffer;
        if (imageBuffer != null)
          serializedImageBuffer = imageBuffer._AsSerializable(compressionLevel);
    
        serializedFrame.CapturedImageBuffer = serializedImageBuffer;
      }
    
      if (includeAwarenessBuffers)
      {
        _SerializableDepthBuffer serializedDepthBuffer = null;
        _SerializableSemanticBuffer serializedSemanticBuffer = null;
    
        IDepthBuffer depthBuffer = source.Depth;
        if (depthBuffer != null)
          serializedDepthBuffer = depthBuffer._AsSerializable();
    
        ISemanticBuffer semanticBuffer = source.Semantics;
        if (semanticBuffer != null)
          serializedSemanticBuffer = semanticBuffer._AsSerializable();
    
        serializedFrame.DepthBuffer = serializedDepthBuffer;
        serializedFrame.SemanticBuffer = serializedSemanticBuffer;
      }

      if (includeFeaturePoints)
        serializedFrame.RawFeaturePoints = source.RawFeaturePoints._AsSerializable();

      return serializedFrame;
    }

    private _SerializableARFrame _SerializeWithoutBuffers(IARFrame source)
    {
      var serializedAnchors =
      (
        from anchor in source.Anchors
        select anchor._AsSerializable()
      ).ToArray();

      var estimatedDisplayTransform =
        source.CalculateDisplayTransform
        (
          Screen.orientation,
          Screen.width,
          Screen.height
        );

      var serializableMaps =
      (
        from map in source.Maps
        select map._AsSerializable()
      ).ToArray();

      return
        new _SerializableARFrame
        (
          null,
          null,
          null,
          source.Camera._AsSerializable(),
          null,
          serializedAnchors.AsNonNullReadOnly<IARAnchor>(),
          serializableMaps,
          source.WorldScale,
          estimatedDisplayTransform
        );
    }
  }
}
