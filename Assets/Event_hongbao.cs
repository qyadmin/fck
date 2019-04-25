using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_hongbao : MonoBehaviour {

    [SerializeField]
    Transform hongbao;
    public void isHongbao()
    {
        Debug.Log(Static.Instance.GetValue("hb_state"));
        if (Static.Instance.GetValue("hb_state") != "0")
        {
            hongbao.gameObject.SetActive(true);
        }
        else
            hongbao.gameObject.SetActive(false);
    }
}
