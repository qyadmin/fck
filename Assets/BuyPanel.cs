using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour {

    [SerializeField]
    Text Integer;
    [SerializeField]
    InputField input;
    public void GetInt(Dropdown num)
    {
        input.text = num.captionText.text;
        Integer.text = "X"+(int.Parse(input.text)/5000);
    }

}
