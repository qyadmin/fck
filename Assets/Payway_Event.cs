using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Text.RegularExpressions;

public class Payway_Event : MonoBehaviour {

    [SerializeField]
    Dropdown payway;

    [SerializeField]
    Transform qr_Code;
    [SerializeField]
    Transform bank;

    [SerializeField]
    Text paytype;

    public void Reset_pay()
    {
        payway.value = 0;
        paytype.text = (payway.value+1).ToString();
        Static.Instance.AddValue("type", (payway.value + 1).ToString());
        reset_transform();
    }

    public void valueChange()
    {
        paytype.text = (payway.value + 1).ToString();
        Static.Instance.AddValue("type", (payway.value + 1).ToString());
        reset_transform();
    }

    void reset_transform()
    {
        qr_Code.gameObject.SetActive(false);
        bank.gameObject.SetActive(false);

        if (payway.value == 0)
            bank.gameObject.SetActive(true);
        if (payway.value == 1 || payway.value == 2)
            qr_Code.gameObject.SetActive(true);

    }

    [SerializeField]
    Text img_url;
    [SerializeField]
    Text idcard, name;
    [SerializeField]
    Camera_Contral loadimage;
    public void GetJson(JsonData json)
    {
        JsonData data = json["data"];

        if (payway.value == 0)
        {
            idcard.text = Regex.Unescape(JsonMapper.ToJson(data["img"]).Replace("\"", ""));
            name.text = Regex.Unescape(JsonMapper.ToJson(data["name"]).Replace("\"", ""));
        }
        if (payway.value == 1 || payway.value == 2)
        {
            img_url.text = Regex.Unescape(JsonMapper.ToJson(data["img"]).Replace("\"", ""));
            loadimage.LoadImage(img_url);
        }
            

    }
}
