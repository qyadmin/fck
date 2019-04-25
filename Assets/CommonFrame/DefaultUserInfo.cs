using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonFrame
{

    [System.Serializable]
    public class TextInfo
    {
        public string Key;
        public UguiTextAlter[] TextValues;
    }

    [System.Serializable]
    public class ImageInfo
    {
        public string Key;
        public UguiImageAlter[] ImageValues;
    }

    public class DefaultUserInfo : MonoBehaviour
    {
        [Tooltip("昵称")]
        public UguiTextAlter NickName;
        [Tooltip("名字")]
        public UguiTextAlter UserName;
        [Tooltip("ID")]
        public UguiTextAlter Id;
        [Tooltip("今日收益")]
        public UguiTextAlter TodayEarning;
        [Tooltip("余额")]
        public UguiTextAlter Balance;
        [Tooltip("头像")]
        public UguiImageAlter HeadPortrait;
        [Tooltip("金币")]
        public UguiTextAlter Gold;
        [Tooltip("银行内的金币")]
        public UguiTextAlter BankGold;
        [Tooltip("另外添加的文本信息")]
        public TextInfo[] OtherTextInfos;
        [Tooltip("另外添加的图片信息")]
        public ImageInfo[] OtherImageInfos;

        public DefaultUserData UserData;

        public void InitUserData()
        {
            UserData = new DefaultUserData();
            UserData.InitUserData();
        }

        void Start()
        {

        }
    }
}