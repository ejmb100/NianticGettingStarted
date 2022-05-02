// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;
using System.Runtime.InteropServices;

namespace Niantic.ARDK.Recording
{
  /// <summary>
  /// Preview config for recordings
  /// </summary>
  [Obsolete("This will be removed in a future release, use ARCapture classes instead")]
  [StructLayout(LayoutKind.Sequential)]
  public struct ARRecordingPreviewConfig
  {
    /// <summary>
    /// Configuration for encoding the preview video
    /// </summary>
    public AREncodingConfig EncodingConfig;

    /// <summary>
    /// Creates an ARRecordingPreviewConfig
    /// </summary>
    /// <param name="encodingConfig">Configuration for encoding the preview video</param>
    public ARRecordingPreviewConfig(AREncodingConfig encodingConfig)
    {
      EncodingConfig = encodingConfig;
    }
  }
}