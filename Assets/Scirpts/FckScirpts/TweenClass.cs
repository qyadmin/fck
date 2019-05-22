using System;

[Serializable]public class TweenerClass
{
    public float DurationTime;

    public float DelayTime;

    public ExeType AnimType;

    public CoordinateType AnimCoordType;
}public enum ExeType
{
    Once,
    Loop,
    PinpPong
}public enum CoordinateType
{
    Local,
    Word
}
