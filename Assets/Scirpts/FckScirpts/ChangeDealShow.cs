using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDealShow : MonoBehaviour
{
    public Dropdown[] DealDrops;
    /// <summary>
    /// 变换值调用的事件
    /// </summary>
    public trade_Event DealTrade;

    /// <summary>
    /// 在Drop变换时给需要赋值的Input赋值
    /// </summary>
    /// <param name="num">0为买入的Drop和Input.1为卖出的Drop和Input</param>
    public void OnValueChange(int num)
    {
        if (DealDrops.Length <= num)
        {
            Debug.Log("传入的num值使DropDown数组越界!");
            return;
        }
        if (DealDrops[num].value == 2)
        {
            Static.Instance.AddValue("flag", "0");
        }
        else
        {
            if (num == 0)
                DealTrade.flag_valuechange();
            else if (num == 1)
                DealTrade.flag2_valuechange();
        }
    }
}
