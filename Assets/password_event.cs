using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class password_event : MonoBehaviour {

    [SerializeField]
    InputField _old,_new;


    public void validation()
    {
        if (_old.text == string.Empty || _new.text == string.Empty)
        {
            ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            ShowOrHit._Instance.msg.text = "请填写完整信息";
            return;
        }
        else
        {
            if (_old.text != _new.text)
            {
                ShowOrHit._Instance.Worning.gameObject.SetActive(true);
                ShowOrHit._Instance.msg.text = "两次输入的密码不一致";
            }
            else {
                this.GetComponent<HttpModel>().Get();
                _new.text = string.Empty;
            }               
        }

        
    }

}
