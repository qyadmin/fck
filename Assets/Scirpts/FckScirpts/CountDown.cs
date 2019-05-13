using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 倒计时并显示进度条
/// </summary>
public class CountDown : MonoBehaviour
{
    /// <summary>
    /// 倒计时进度条
    /// </summary>
    public Slider CountDownSlider;
    /// <summary>
    /// 倒计时文本
    /// </summary>
    public Text CountDownText;
    /// <summary>
    /// 一天的秒数
    /// </summary>
    private const float _oneDay = 86400f;
    /// <summary>
    /// 接收的剩余时间
    /// </summary>
    private int _surplusTime;
    private int _tempTime = 0;

    public GameObject ShowDownText;
    public GameObject HideDownText;
    public Transform Parent;

    public GameObject RedHint;

    /// <summary>
    /// 通过字段获得剩余时间,并进行倒计时
    /// </summary>
    public void GetSurplusTime()
    {
        if (Parent != null)
        {
            var item = Parent.GetComponentsInChildren<buysellOrders_event>();
            foreach (var t in item)
            {
                t.ChangeButtonEnable();
            }
        }
        _surplusTime = int.Parse(Static.Instance.GetValue("time_mining"));

        var num = int.Parse(Static.Instance.GetValue("can_sign"));
        if (num > 0)
            RedHint.SetActive(true);
        else
            RedHint.SetActive(false);

        if (_surplusTime < 0)
        {
            ShowDownText.SetActive(false);
            HideDownText.SetActive(true);
            CountDownSlider.value = 1;
            return;
        }
        else
        {
            ShowDownText.SetActive(true);
            HideDownText.SetActive(false);
        }
        var hasRatio = _surplusTime / _oneDay;
        Debug.Log(_surplusTime);
        if (CountDownSlider != null && hasRatio <= 1)
        {
            CountDownSlider.value = 1 - hasRatio;
            CancelInvoke("ChangeTimeFormat");
            _tempTime = 0;
            if (hasRatio == 1)
                return;
            InvokeRepeating("ChangeTimeFormat", 0, 1);
        }
    }

    /// <summary>
    /// 显示时间并倒计时
    /// </summary>
    private void ChangeTimeFormat()
    {
        if (_surplusTime == 0)
        {
            CancelInvoke("ChangeTimeFormat");
            return;
        }
        CountDownText.text = CountTime(_surplusTime);
        if (_tempTime >= 864)
        {
            var hasRatio = _surplusTime / _oneDay;
            if (CountDownSlider != null && hasRatio <= 1)
            {
                CountDownSlider.value = 1 - hasRatio;
            }
        }
        _surplusTime--;
        _tempTime++;
    }

    /// <summary>
    /// 把传过来的剩余时间换成00:00:00格式
    /// </summary>
    /// <param name="value">剩余时间</param>
    /// <returns></returns>
    private string CountTime(int value)
    {
        string str = "";
        var hour = value / 3600;
        var min = value % 3600 / 60;
        var sec = value % 60;
        var hourStr = hour < 10 ? "0" + hour : hour.ToString();
        var minStr = min < 10 ? "0" + min : min.ToString();
        var secStr = sec < 10 ? "0" + sec : sec.ToString();
        str = hourStr + ":" + minStr + ":" + secStr;
        return str;
    }
}
