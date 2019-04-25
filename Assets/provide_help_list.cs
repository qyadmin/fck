using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class provide_help_list : MonoBehaviour
{
    [SerializeField]
    Button self;

    public enum type
    {
        once,
        second
    }

    [SerializeField]
    type types;

    [SerializeField]
    Image active;
    public void help_list_event(string obj)
    {

        if (obj == "请确认")
            this.GetComponent<Text>().color = HexToColor("E99A42FF");


        if (types == type.second)
        {
            if (obj == "匹配中")
            {
                self.interactable = false;
                if (active != null) active.color = HexToColor("FF8920FF");
            }
            else
            {
                self.interactable = true;
                if (active != null) active.color = HexToColor("20FF34FF");
            }
        }
        if (types == type.once)
        {
            if (obj == "匹配中")
            {
                self.interactable = false;
                if (active != null) active.color = HexToColor("FF8920FF");
            }
            else
            {
                self.interactable = true;
                if (active != null) active.color = HexToColor("20FF34FF");
            }
        }
        this.GetComponent<Text>().text = obj + ">";


        self.onClick.AddListener(
            delegate ()
            {
                self.GetComponent<HttpModel>().Get();
            }
            );
    }

    public Color HexToColor(string hex)
    {
        byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        float r = br / 255f;
        float g = bg / 255f;
        float b = bb / 255f;
        float a = cc / 255f;
        return new Color(r, g, b, a);
    }
}
