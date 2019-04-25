using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set_sdflag : MonoBehaviour {

    [SerializeField]
    Text all;

    public void set_sdflag()
    {
        if (Static.Instance.GetValue("sdflag") == "0")
        {
            all.text = "金额必须是1000的倍数 您当前的排单下限是1000元，上限是5000元 排单每1000元消耗1个蓝钻石";
        }
        else
        {
            all.text = "金额必须是1000的倍数 您当前的排单下限是" + Static.Instance.GetValue("sc_money")+ "，上限是20000元 排单每1000元消耗1个蓝钻石";
        }
    }

    [SerializeField]
    Image image1,image2;
    [SerializeField]
    Sprite huang, hong, hei;
    public void SetZhuan()
    {
        switch (Static.Instance.GetValue("jibie"))
        {
            case "0":
                image1.sprite = huang;
                image2.sprite = huang;
                break;
            case "1":
                image1.sprite = hong;
                image2.sprite = hong;
                break;
            case "2":
                image1.sprite = hei;
                image2.sprite = hei;
                break;
                
        }
    }


    Timer _timer = new Timer(1);
    int time;
    [SerializeField]
    Text time_text;
    public void GetTime()
    {
        _timer.EndTimer();
        time = int.Parse(Static.Instance.GetValue("end_time")) - int.Parse(Static.Instance.GetValue("start_time"));
        _timer.tickEvnet += time_event;
        _timer.StartTimer();
    }

    void time_event()
    {
        if (time <= 0)
        {
            time_text.text = "已到期";
            _timer.EndTimer();
        }
        else
        {
            time_text.text = Mathf.Floor(time / 86400) + "天" + Mathf.Floor((time % 86400) / 3600) + "时" + Mathf.Floor((time % 86400 % 3600) / 60) + "分" + Mathf.Floor(time % 86400 % 3600 % 60) + "秒";
            time--;
        }     
    }

    private void Update()
    {
        _timer.UpdateRepeatTimer(Time.deltaTime);
    }
}
