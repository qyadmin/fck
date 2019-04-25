using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class url_event : MonoBehaviour {

    [SerializeField]
    string url;

    public void Openurl()
    {
        Application.OpenURL(url);
    }
}
