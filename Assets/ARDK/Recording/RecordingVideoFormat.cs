// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;

namespace Niantic.ARDK.Recording
{
  [Obsolete("This will be removed in a future release, use ARCapture classes instead")]
  public enum RecordingVideoFormat: byte
  {
    None = 0,
    VP8 = 1,
    VP9 = 2,
    VP9TwoPass = 3,
    VP9Lossless = 4,
    VP9LosslessTwoPass = 5,
  }
}