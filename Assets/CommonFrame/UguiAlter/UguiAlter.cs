using UnityEngine;

namespace CommonFrame
{

    public enum UguiType
    {
        None,
        Set,
        Get,
        SetAndGet
    }

    public interface UguiAlter<T>
    {
        void SetValue(T value);

        void SetValue(string value);

        void SetColorValue(Color colorValue);

        void SetColorValue(string colorValue);

        T GetValue();
    }

    public class AllUguiAlter : MonoBehaviour
    {
        public UguiType Type;

        public bool IsSet()
        {
            return Type == UguiType.Set || Type == UguiType.SetAndGet;
        }

        public bool IsGet()
        {
            return Type == UguiType.Get || Type == UguiType.SetAndGet;
        }
    }
}