using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEditor;
using System.Runtime.InteropServices;

namespace CommonFrame
{
    public enum Platform
    {
        Android,
        IOS,
        PC
    }

    public class SettingEditor : EditorWindow
    {
        private static SettingEditor window;
        private int _gridId;

        /// <summary>
        /// 版本号
        /// </summary>
        private string _versionStr;
        /// <summary>
        /// 打包的名字
        /// </summary>
        private string _packageName;
        /// <summary>
        /// BundleID
        /// </summary>
        private string _bundleId;
        /// <summary>
        /// 服务器Url
        /// </summary>
        private string _urlStr;
        /// <summary>
        /// Icon的路径
        /// </summary>
        private string _iconPath;
        /// <summary>
        /// 平台
        /// </summary>
        private int _platform;
        /// <summary>
        /// Icon的图片
        /// </summary>
        private Texture _iconImage = new Texture();
        /// <summary>
        /// 平台的枚举
        /// </summary>
        private Platform _platformEnum;
        /// <summary>
        /// 保存的数据的路径
        /// </summary>
        private string _infoPath = "/CommonFrame/SettingInfo.txt";

        [MenuItem("Build/Setting/PlayerSetting")]
        private static void CreateWindow()
        {
            window = (SettingEditor)EditorWindow.GetWindow(typeof(SettingEditor), false, "PleyerSetting", false);
            //获取保存的值并赋值
            window.Show();
        }

        private void OnGUI()
        {
            BeginWindows();
            GUILayout.Window(10, new Rect(0, 0, 150, 480), DoWindow, "Tab");
            GUILayout.Window(_gridId, new Rect(150, 0, 500, 480), SettingWindow, "Setting");
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Space(300);
            if (GUILayout.Button("Apply") && _gridId == 0)
            {
                ApplyInfo();
            }
            if (GUILayout.Button("Cancle"))
            {
                Close();
            }
            GUILayout.EndHorizontal();
            EndWindows();
        }

        private void DoWindow(int id)
        {
            _gridId = GUILayout.SelectionGrid(_gridId, new[] { "BuildSetting", "PlayerSetting" }, 1);//, "OtherSetting"
        }

        private void SettingWindow(int id)
        {
            switch (id)
            {
                case 0:
                    BuildSettingWindow();
                    break;
                case 1:
                    PlayerSettingWindow();
                    break;
                //case 2:
                //    OtherSettingWindow();
                //    break;
                default:
                    break;
            }
        }

        private void BuildSettingWindow()
        {
            _versionStr = EditorGUILayout.TextField("Version:", _versionStr, GUILayout.Width(500));
            _packageName = EditorGUILayout.TextField("Package Name:", _packageName, GUILayout.Width(500));
            _bundleId = EditorGUILayout.TextField("Bundle Id:", _bundleId, GUILayout.Width(500));
            _platformEnum = (Platform)EditorGUILayout.EnumPopup("选择发布平台:", _platformEnum, GUILayout.Width(300));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Icon图片:");
            GUILayout.Box(_iconImage, new[] { GUILayout.Height(100), GUILayout.Width(100) });
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            //获得一个长400的框  
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Width(400));
            //将上面的框作为文本输入框  
            _iconPath = EditorGUI.TextField(rect, "IconPath:", _iconPath);
            if (_iconPath != null)
                GetIconImage(_iconPath);
            if (((Event.current.type == EventType.DragUpdated) || (Event.current.type == EventType.DragExited)) && rect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
                {
                    _iconPath = DragAndDrop.paths[0];
                }
            }


            if (GUILayout.Button("浏览", GUILayout.Width(100)))
            {
                OpenFileName openFileName = new OpenFileName();
                openFileName.structSize = Marshal.SizeOf(openFileName);
                openFileName.filter = "png(*.png)\0*.png";
                openFileName.file = new string(new char[256]);
                openFileName.maxFile = openFileName.file.Length;
                openFileName.fileTitle = new string(new char[64]);
                openFileName.maxFileTitle = openFileName.fileTitle.Length;
                openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
                openFileName.title = "窗口标题";
                openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
                if (LocalDialog.GetOpenFileName(openFileName))
                {
                    _iconPath = openFileName.file;
                }
            }
            GUILayout.EndHorizontal();
        }

        private void PlayerSettingWindow()
        {
            GUILayout.Label("空白界面,为了以后添加新功能");
        }

        private void OtherSettingWindow()
        {
            GUILayout.Label("空白界面,为了以后添加新功能");
        }

        private void GetHistoryInfo()
        {
            StreamReader sr;
            var file = new FileInfo(Application.dataPath + _infoPath);
            if (!file.Exists)
                return;
            sr = File.OpenText(Application.dataPath + _infoPath);
            var list = new ArrayList();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            sr.Close();
            sr.Dispose();
            if (list.Count <= 0)
            {
                Debug.Log("SettingInfo文档中没有数据.");
                return;
            }
            string str = null;
            foreach (string item in list)
            {
                str += item;
            }
            JsonData data = JsonMapper.ToObject(str);
            _versionStr = data.Keys.Contains("version") ? data["version"].ToString() : "";//版本号
            _packageName = data.Keys.Contains("name") ? data["name"].ToString() : "";//打包名
            _bundleId = data.Keys.Contains("bundle") ? data["bundle"].ToString() : "";//BundleId
            _urlStr = data.Keys.Contains("url") ? data["url"].ToString() : "";//Url
            _iconPath = data.Keys.Contains("icon") ? data["icon"].ToString() : "";//Icon地址
            _platform = data.Keys.Contains("platform") && string.IsNullOrEmpty(data["platform"].ToString()) ? int.Parse(data["platform"].ToString()) : 0;//打包平台
            _platformEnum = (Platform)_platform;
        }

        private void GetIconImage(string path)
        {
            if (path.EndsWith(".png"))
            {
                _iconImage = null;
                return;
            }
            Texture2D tx = new Texture2D(100, 100);
            tx.LoadImage(GetImageByte(path));
            _iconImage = tx;
        }

        private byte[] GetImageByte(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            if (fs.Length < 0) return null;
            var img = new byte[fs.Length];
            fs.Read(img, 0, img.Length);
            fs.Close();
            return img;
        }

        private void ApplyInfo()
        {
            if (string.IsNullOrEmpty(_versionStr) || _versionStr.Contains("*.*.*"))
            {
                Debug.Log("输入的版本号格式不对或者为空,应输入1.0.1");
                return;
            }
            if (string.IsNullOrEmpty(_packageName))
            {
                Debug.Log("打包名不可以为空!");
            }
            if (string.IsNullOrEmpty(_bundleId) || _bundleId.Contains("*.*.*"))
            {
                Debug.Log("输入的BundleId格式不对或者为空,应输入xxx.xxxx.xxxx");
                return;
            }
            if (_urlStr.EndsWith("\\"))
            {
                Debug.Log("输入的Url的格式不是以\\结尾,请重新输入!");
                return;
            }
            if (_iconPath.EndsWith(".png"))
            {
                Debug.Log("选择的Icon的格式不是png格式,请重新选择!");
                return;
            }

            //PlayerSettings.

            //把数据保存到json文件中
            Hashtable table = new Hashtable();
            table.Add("version", _versionStr);
            table.Add("name", _packageName);
            table.Add("bundle", _bundleId);
            table.Add("url", _urlStr);
            table.Add("icon", _iconPath);
            table.Add("platform", (int)_platformEnum);
        }
    }
}