using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePassword : MonoBehaviour {

    [SerializeField]
    InputField password,rep_password;

    [SerializeField]
    HttpModel http;
    public void check()
    {
        if (password.text != rep_password.text)
        {
            ShowOrHit._Instance.Worning.gameObject.SetActive(true);
            ShowOrHit._Instance.msg.text = "两次输入的新密码不一致";
            return;
        }
        http.Get();
    }
}
