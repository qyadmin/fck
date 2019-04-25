using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonFrame
{
    [RequireComponent(typeof(Text))]
    public class UguiTextAlter : AllUguiAlter, UguiAlter<string>
    {
        private Text _text;

        void Start()
        {
            _text = GetComponent<Text>();
        }

        public void SetValue(string value)
        {
            _text.text = value;
        }

        public void SetColorValue(Color colorValue)
        {
            _text.color = colorValue;
        }

        public void SetColorValue(string colorValue)
        {
            _text.color = Common.HexToColor(colorValue);
        }

        public string GetValue()
        {
            return _text.text;
        }
    }
}