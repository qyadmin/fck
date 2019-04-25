using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonFrame
{
    [RequireComponent(typeof(Image))]
    public class UguiImageAlter : AllUguiAlter, UguiAlter<Sprite>
    {
        private Image _image;

        void Start()
        {
            _image = GetComponent<Image>();
        }

        private IEnumerator LoadImageByUrl(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www != null && string.IsNullOrEmpty(www.error))
            {
                Sprite value = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
                SetValue(value);
            }
        }

        public void SetValue(string value)
        {
            StartCoroutine(LoadImageByUrl(value));
        }

        public void SetColorValue(Color colorValue)
        {
            _image.color = colorValue;
        }

        public void SetColorValue(string colorValue)
        {
            _image.color = Common.HexToColor(colorValue);
        }

        public void SetValue(Sprite value)
        {
            _image.sprite = value;
        }

        public Sprite GetValue()
        {
            return _image.sprite;
        }
    }
}