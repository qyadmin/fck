using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonFrame
{
    /// <summary>
    /// 需要显示的窗口
    /// 挂脚本的物体显示,子物体命名为window并隐藏,所有显示物体在window显示
    /// </summary>
    public class DefaultWindow : MonoBehaviour
    {
        public string Action;
        public ScrollRect RectView;
        public DefaultListItem Item;

        private GameObject _window;

        void Awake()
        {
            _window = transform.Find("window").gameObject;
            if (_window == null)
                Debug.LogError("");
        }

        public void Show()
        {
            //发送并接收消息
            _window.SetActive(true);
        }

        public void Hide()
        {
            _window.SetActive(false);
        }

        public void OpenWindow(GameObject go)
        {
            if (SceneManager.Singleton != null)
            {
                SceneManager.Singleton.OpenWindow(go.name);
            }
            else
                Debug.Log("SceneManager的单例为空!");
        }
    }
}