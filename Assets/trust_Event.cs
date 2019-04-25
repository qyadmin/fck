using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trust_Event : MonoBehaviour {

    [SerializeField]
    Dropdown type;

    public void set_flag()
    {
        type.value = 0;
        Static.Instance.AddValue("fbday", "10");
        Debug.Log(Static.Instance.GetValue("fbday"));
    }


    public void flag_valuechange()
    {
        Static.Instance.AddValue("fbday", ((type.value + 1)*10).ToString());
        Debug.Log(Static.Instance.GetValue("fbday"));
    }
    [SerializeField]
    Transform zhuangtai1, zhuangtai0;
    public void GetZhuangtai(string value)
    {
        zhuangtai0.gameObject.SetActive(false);
        zhuangtai1.gameObject.SetActive(false);
        if (value == "0")
            zhuangtai1.gameObject.SetActive(true);
        if (value == "1")
            zhuangtai0.gameObject.SetActive(true);
    }
    [SerializeField]
    Transform iskaifang;
    public void detection(Text value)
    {
        if (value.text == "1")
        {
            iskaifang.gameObject.SetActive(false);
        }
        if (value.text == "0")
        {
            iskaifang.gameObject.SetActive(true);
        }

    }
}
