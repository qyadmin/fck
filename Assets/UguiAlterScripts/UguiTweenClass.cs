using System;

namespace UguiAlter
{
    [Serializable]
    public class UguiTweenClass
    {
        public float DurationTime;

        public float DelayTime;

        public ExeType AnimType;

        public CoordinateType AnimCoordType;

        public RotateType RotateAnimType;
    }

    public enum ExeType
    {
        Once,
        Loop,
        PinpPong
    }

    public enum CoordinateType
    {
        Local,
        Word
    }


    public enum RotateType
    {
        RotateX,
        RotateY,
        RotateZ
    }
}
