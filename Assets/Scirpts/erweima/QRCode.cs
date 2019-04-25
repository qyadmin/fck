using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class QRCode : MonoBehaviour
{

    [SerializeField]
    private Button Btn;
    [SerializeField]
    private HttpModel ereima;
    [SerializeField]
    RawImage img;
    void Start()
    {
        Btn.onClick.AddListener(Show);
    }

    private void Show()
    {
        ereima.CallBackData.Addlistener(delegate (JsonData data)
        {
            LoadImage.GetLoadIamge.Load(data["qrcode"].ToString(), new RawImage[] { img });
        });
        ereima.Get();
    }
}
