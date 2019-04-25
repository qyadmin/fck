using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bank_event : MonoBehaviour {

    [SerializeField]
    Dropdown banklist;

    public void getjson(object bank)
    {
        for (int i =0;i< banklist.options.Count;i++)
        {
            
            if (bank.ToString() == banklist.options[i].text)
            {
                banklist.value = i;
                Debug.Log(bank.ToString() + "   " + banklist.options[i].text +i);
                break;
            }
        }
    }

}
