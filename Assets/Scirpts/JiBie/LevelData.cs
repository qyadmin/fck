using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class LevelData : MonoBehaviour
{



    [SerializeField]
    string[] level;
    [SerializeField]
    Text lv, uplv;
    [SerializeField]
    HttpModel HTTP;

    public void GetData()
    {
        int num = int.Parse(Static.Instance.GetValue("jibie"));
        string str = level[num];     
        lv.text = str;
        if(num>=level.Length-1)
            uplv.text = string.Format("你已达到最高等级");
        else
        uplv.text = string.Format("是否确认升级到{0}", level[num+1]);
    }
}
