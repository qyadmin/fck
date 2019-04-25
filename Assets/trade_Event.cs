using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trade_Event : MonoBehaviour
{

    [System.Serializable]
    public class total
    {
        public Text price;
        public Text total_price;
        public InputField number;
    }
    [SerializeField]
    total total_list1, total_list2;

    [SerializeField]
    Dropdown flag;
    [SerializeField]
    Dropdown flag2;
    [SerializeField]
    Button click_button, click_button2;



    private void Start()
    {
        click_button.onClick.AddListener(delegate ()
        {
            Click();
        });

        click_button2.onClick.AddListener(delegate ()
        {
            Click2();
        });
    }

    public void set_total_price()
    {
        if (total_list1.number.text != string.Empty)
            total_list1.total_price.text = (float.Parse(total_list1.number.text) * float.Parse(total_list1.price.text)).ToString();
    }

    public void set_total2_price()
    {
        if (total_list2.number.text != string.Empty)
            total_list2.total_price.text = (float.Parse(total_list2.number.text) * float.Parse(total_list2.price.text)).ToString();
    }

    public void set_flag()
    {
        flag.value = 0;
        Static.Instance.AddValue("flag", "1");
        Debug.Log(Static.Instance.GetValue("flag"));
    }


    public void set_flag2()
    {
        flag2.value = 0;
        Static.Instance.AddValue("flag", "1");
        Debug.Log(Static.Instance.GetValue("flag"));
    }

    public void flag_valuechange()
    {
        Static.Instance.AddValue("flag", (flag.value + 1).ToString());
        Debug.Log(Static.Instance.GetValue("flag"));
    }

    public void flag2_valuechange()
    {
        Static.Instance.AddValue("flag", (flag2.value + 1).ToString());
        Debug.Log(Static.Instance.GetValue("flag"));
    }

    [SerializeField]
    HttpModel Pay_way, Pay_way2;
    public void Click()
    {
        if (total_list1.number.text == string.Empty || int.Parse(total_list1.number.text) <= 0)
        {
            ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            ShowOrHit._Instance.msg.text = "请输入正确数量";
        }
        else
        {
            //ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            //ShowOrHit._Instance.msg.text = "【交易事项：当您收到订单匹配成功短信时，请您在40分钟内给卖方付款，若超时未付款，系统将扣除您买入数量1%的币转给卖方，且信用分下降2分。】";


            ShowOrHit._Instance.Password_panle.gameObject.SetActive(true);
            ShowOrHit._Instance.Password_button.onClick.RemoveAllListeners();

            ShowOrHit._Instance.Password_button.onClick.AddListener(delegate () { Pay_way.Get(); });
        }
    }

    public void Click2()
    {
        if (total_list2.number.text == string.Empty || int.Parse(total_list2.number.text) <= 0)
        {
            ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            ShowOrHit._Instance.msg.text = "请输入正确数量";
        }
        else
        {
            ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            ShowOrHit._Instance.msg.text = "【交易事项：当您收到该笔订单款项时，请您立刻登录APP给买方确认收款，否则系统在40分钟后确认该笔订单付款成功。若您未收到款项，可拨打对方电话确认，如对方拒绝付款，请您立刻投诉。】";


            ShowOrHit._Instance.Password_panle.gameObject.SetActive(true);
            ShowOrHit._Instance.Password_button.onClick.RemoveAllListeners();

            ShowOrHit._Instance.Password_button.onClick.AddListener(delegate () { Pay_way2.Get(); });
        }

    }
}
