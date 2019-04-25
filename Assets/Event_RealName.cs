using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_RealName : MonoBehaviour {

    [SerializeField]
    HttpModel http;

    [SerializeField]
    Transform panleUI;
    public void is_realname()
    {
        switch (Static.Instance.GetValue("smrz_status"))
        {
            case "0":
                ShowOrHit._Instance.Worning.gameObject.SetActive(true);
                ShowOrHit._Instance.msg.text = "【您未实名认证，请尽快实名】";

                panleUI.gameObject.SetActive(true);
                http.Get();
                break;
            case "1":
                ShowOrHit._Instance.Worning.gameObject.SetActive(true);
                ShowOrHit._Instance.msg.text = "【资料已上传，等待审核】";
                break;
            case "2":
                ShowOrHit._Instance.Worning.gameObject.SetActive(true);
                ShowOrHit._Instance.msg.text = "【已认证】";
                break;
            case "3":
                ShowOrHit._Instance.Worning.gameObject.SetActive(true);
                ShowOrHit._Instance.msg.text = "【实名认证已拒绝，请重新实名】";

                panleUI.gameObject.SetActive(true);
                http.Get();
                break;
        }
    }
}
