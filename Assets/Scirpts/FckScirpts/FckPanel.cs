﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WalletInfo
{
    public string TitleName;
    public Sprite TargetSprite;
    public bool HaveOther;
}


public class FckPanel : MonoBehaviour
{
    /// <summary>
    /// 需要获得的key值
    /// </summary>
    public string[] TargetNames;
    /// <summary>
    /// 需要显示或隐藏的物体
    /// </summary>
    public GameObject[] TargetObjects;
    public Text TitleText;
    public Image TargetImage;
    public List<WalletInfo> WalletInfos;
    public Transform Parent;

    public GameObject OtherObject;
    /// <summary>
    /// 根据获得值,来变换物体的状态
    /// </summary>
    public void ChangeTargetState()
    {
        for (int i = 0; i < TargetNames.Length; i++)
        {
            var str = Static.Instance.GetValue(TargetNames[i]);
            if (string.IsNullOrEmpty(str))
            {
                TargetObjects[i].SetActive(false);
                continue;
            }
            var value = int.Parse(str);
            if (value > 0)
                TargetObjects[i].SetActive(true);
            else
                TargetObjects[i].SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public void ShowWalletInfo()
    {
        if (TitleText == null || TargetImage == null)
            return;
        foreach (var info in WalletInfos)
        {
            if (TitleText.text == info.TitleName)
            {
                TargetImage.sprite = info.TargetSprite;
                if (info.HaveOther)
                {
                    OtherObject.transform.SetParent(transform);
                    OtherObject.transform.localPosition = new Vector3(320, 0, 0);
                }
            }
        }
    }

    public void AdjustListItem()
    {
        if (Parent == null) return;
        var items = Parent.GetComponentsInChildren<FckPanel>();
        foreach (var i in items)
        {
            i.ShowWalletInfo();
        }
    }
}
