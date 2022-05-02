// Copyright 2021 Niantic, Inc. All Rights Reserved.

using System;

namespace Niantic.ARDK.Recording
{
  [Obsolete("This will be removed in a future release, use ARCapture classes instead")]
  public enum ARRecordingStatus:
    byte
  {
    Uninitialized = 0,
    Processing = 1,
    Completed = 2,
    Canceled = 3,
    FileCorrupted = 4,
    ConfigError = 5,
    RecordingError = 6,
    EncoderError = 7,
  }
}