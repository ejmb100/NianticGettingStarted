// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;

using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.Utilities.Logging;
using Niantic.ARDK.VirtualStudio.Remote;

using UnityEngine;

#if UNITY_EDITOR
using Niantic.ARDK.Utilities.Extensions;

using UnityEditor;
#endif

namespace Niantic.ARDK.VirtualStudio.AR.Mock
{
  public sealed class MockSceneConfiguration:
    MonoBehaviour
  {
#if UNITY_EDITOR
    private void OnEnable()
    {
      var layerName = _MockFrameBufferProvider.MOCK_LAYER_NAME;
      var mockLayer = LayerMask.NameToLayer(layerName);
      if (mockLayer < 0)
      {
        if (!_MockFrameBufferProvider.CreateLayer(layerName, out mockLayer))
          return;
      }

      var noLayerCount = 0;
      foreach (var descendant in gameObject.GetComponentsInChildren<Transform>())
      {
        if (descendant.gameObject.layer != mockLayer)
          noLayerCount++;
      }

      if (noLayerCount > 0)
      {
        ARLog._WarnFormatRelease
        (
          "Found {0} GameObjects parented to {1} that are not in the " +
          "Layer: {2} required for use of Virtual Studio Mock mode.\n" +
          "Reset the MockSceneConfiguration to set " +
          "the correct layer for all objects in its hierarchy.",
          noLayerCount,
          gameObject.name,
          layerName
        );
      }
    }

    private void Reset()
    {
      _SetLayerForDescendants();
    }

    [MenuItem("GameObject/3D Object/ARDK/MockScene", false, 0)]
    private static void CreateRoot(MenuCommand menuCommand)
    {
      var go = new GameObject("MockSceneRoot");
      var mockScene = go.AddComponent<MockSceneConfiguration>();
      mockScene._SetLayerForDescendants();

      // Ensure it gets re-parented if this was a context click (otherwise does nothing)
      GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);

      // Register the creation in the undo system
      Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

      Selection.activeObject = go;
    }

    // Sets the layer of this component's GameObject and all its descendants
    // to _MockFrameBufferProvider.MOCK_LAYER_NAME.
    // It will add that layer to the TagManager.asset if it does not already exist.
    internal void _SetLayerForDescendants()
    {
      var layerName = _MockFrameBufferProvider.MOCK_LAYER_NAME;
      var layerIndex = LayerMask.NameToLayer(layerName);
      if (layerIndex < 0)
      {
        if (!_MockFrameBufferProvider.CreateLayer(layerName, out layerIndex))
          return;
      }

      foreach (var descendant in gameObject.GetComponentsInChildren<Transform>())
        descendant.gameObject.layer = layerIndex;
    }
#endif
  }
}

