using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class image_Event : MonoBehaviour {

    [SerializeField]
    Image bigimage;
    [SerializeField]
    Image smartimage;

    public void click()
    {
        bigimage.sprite = smartimage.sprite;
    }
}
