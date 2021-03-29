using System;


public enum MemoryInGB
{
    All = 0,
    FreeLeft = 1,
    Used = 2
}

public enum ErrorsType
{
    All = 0,
    ArgumentException = 1,
    ArgumentNullException = 2,
    ArgumentOutOfRangeException = 3,
    DirectoryNotFoundException = 4,
    DivideByZeroException = 5,
    DriveNotFoundException = 6,
    FileNotFoundException = 7,
    FormatException = 8,
    IndexOutOfRangeException = 9,
    InvalidOperationException = 10,
    KeyNotFoundException = 11,
    NotSupportedException = 12,
    OverflowException = 13,
    PathTooLongException = 14,
    PlatformNotSupportedException = 15,
    RankException = 16,
    TimeoutException = 17,
    UriFormatException = 18
}

public enum Percentile
{
    Median = 0,
    P75 = 1,
    P90 = 2,
    P95 = 3,
    P99 = 4
}