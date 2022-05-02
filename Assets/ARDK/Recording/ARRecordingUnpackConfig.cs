// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;
using System.Runtime.InteropServices;

namespace Niantic.ARDK.Recording
{
  /// <summary>
  /// Configs for unpacking the frames to a directory.
  /// </summary>
  [Obsolete("This will be removed in a future release, use ARCapture classes instead")]
  [StructLayout(LayoutKind.Sequential)]
  public struct ARRecordingUnpackConfig
  {
    /// <summary>
    /// The target destination file path for AR data. This should end with .gz
    /// </summary>
    [MarshalAs(UnmanagedType.LPStr)]
    public string UnpackDestination;
  }
}