using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonFrame
{
    /// <summary>
    /// 挂在所有弹窗的父物体上,用于寻找窗口以及显示窗口
    /// </summary>
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Singleton;
        private void Awake()
        {
            Singleton = this;
        }

        public void OpenWindow(string name)
        {
            var childs = this.GetComponentsInChildren<DefaultWindow>();
            foreach (var item in childs)
            {
                if (item.gameObject.name == name)
                {
                    item.Show();
                    return;
                }
            }
            Debug.Log("没有找到窗口名为" + name + "的窗口");
        }
    }
}