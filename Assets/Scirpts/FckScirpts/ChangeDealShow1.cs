using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DealNeedChange
{
    public GameObject[] Needs;
}

public class ChangeDealShow1 : MonoBehaviour
{
    /// <summary>
    /// 索引0为默认显示,1为特殊显示
    /// </summary>
    public List<DealNeedChange> NeedChanges;
    /// <summary>
    /// 需要获取值的下拉数组.0位买入1位卖出
    /// </summary>
    public Dropdown[] DropValues;
    /// <summary>
    /// 需要赋值的输入框数组,0位买入1位卖出
    /// </summary>
    public InputField[] InputValues;
    /// <summary>
    /// 变换值调用的事件
    /// </summary>
    public trade_Event DealTrade;

    /// <summary>
    /// 根据Index中的sdfalg值来隐藏显示物体.0为特殊显示的,1为默认的
    /// </summary>
    public void GetFlag()
    {
        var flag = Static.Instance.GetValue("sdflag");
        Debug.Log("sdflag为:" + flag);
        if (flag == "0")
        {
            Change(1);
            Static.Instance.AddValue("flag", "0");
            foreach (var item in InputValues)
            {
                item.text = "100";
            }
            DealTrade.set_total_price();
            DealTrade.set_total2_price();
        }
        else
        {
            Change(0);
        }
    }

    /// <summary>
    /// 在变换时给需要赋值的Input赋默认值
    /// </summary>
    /// <param name="num">0为给买入赋值1为给卖出赋值</param>
    private void SetDropDefaultValue(int num)
    {
        InputValues[num].text = DropValues[num].captionText.text;
        if (num == 0)
            DealTrade.set_total_price();
        else if (num == 1)
            DealTrade.set_total2_price();
    }

    /// <summary>
    /// 根据传来的值来显示隐藏物体
    /// </summary>
    /// <param name="num">需要显示的索引</param>
    private void Change(int num)
    {
        for (int i = 0; i < NeedChanges.Count; i++)
        {
            if (i == num)
            {
                foreach (var item in NeedChanges[i].Needs)
                {
                    item.SetActive(true);
                }
            }
            else
            {
                foreach (var item in NeedChanges[i].Needs)
                {
                    item.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 在Drop变换时给需要赋值的Input赋值
    /// </summary>
    /// <param name="num">0为买入的Drop和Input.1为卖出的Drop和Input</param>
    public void OnValueChange(int num)
    {
        if (InputValues.Length > num && InputValues[num] != null && num >= 0)
        {
            InputValues[num].text = DropValues[num].captionText.text;
            if (num == 0)
                DealTrade.set_total_price();
            else if (num == 1)
                DealTrade.set_total2_price();
        }
    }
}
