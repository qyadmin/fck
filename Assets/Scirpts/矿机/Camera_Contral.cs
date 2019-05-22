using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class wwwform
{
    public string key;
    public Text value;
    public InputField input_value;
    public bool isSave;

    public void set()
    {
        if (isSave)
        {
            if (value != null)
                Static.Instance.AddValue(key, value.text);
            if (input_value != null)
                Static.Instance.AddValue(key,input_value.text);
        }
    }
}


public class Camera_Contral : MonoBehaviour {

    [SerializeField]
    AndroidPhoto android;
    [SerializeField]
    IOSPhoto ios;


    [SerializeField]
    public string url;

    [SerializeField]
    public string image_name = "img_url";
    [SerializeField]
    public string image_name2 = "img_url2";
    public wwwform[] others;

    [SerializeField]
    public bool istogetor = true;

    [SerializeField]
    public string worning;

    public void OpenPhoto()
    {
#if UNITY_ANDROID
        android.contral_ = this;
        android.OpenPhoto();


#elif UNITY_IPHONE
        ios.contral_ = this;
        ios.OpenPhoto();
#endif
    }

    public void OpenCamera()
    {
#if UNITY_ANDROID
        android.contral_ = this;
        android.OpenCamera();
#elif UNITY_IPHONE
        ios.contral_ = this;
        ios.OpenCamera();
#endif
    }

    public void http_get()
    {
#if UNITY_ANDROID
        android.contral_ = this;
        android.http_get();
#elif UNITY_IPHONE
        ios.contral_ = this;
        ios.http_get();
#endif
    }

    public void SetPicNum(int i)
    {
#if UNITY_ANDROID
        android.picNum = i;
#elif UNITY_IPHONE
        ios.picNum = i;
#endif
    }


    public void reset_pic()
    {
#if UNITY_ANDROID
        android.contral_ = this;
        android.base64String = string.Empty;
        android.base64String2 = string.Empty;
        android.picNum = 1;
        foreach (Image i in sprit)
        {
            i.sprite = resetsprit;
        }
        foreach (Image i in sprit2)
        {
            i.sprite = resetsprit2;
        }




#elif UNITY_IPHONE
        ios.contral_ = this;
        ios.base64String = string.Empty;
        ios.base64String2 = string.Empty;
        ios.picNum = 1;
        foreach (Image i in sprit)
        {
            i.sprite = resetsprit;
        }
        foreach (Image i in sprit2)
        {
            i.sprite = resetsprit2;
        }
#endif
    }


    public UnityEvent Suc, Fal;


    [SerializeField]
    public Image[] sprit;
    [SerializeField]
    public Image[] sprit2;

    [SerializeField]
    public Sprite resetsprit;
    [SerializeField]
    public Sprite resetsprit2;
    public void LoadImage(Text obj)
    {
        if (obj.text != null)
        {
            StartCoroutine(loadtexture(obj.text));
        }

    }

    public void LoadImage2(Text obj)
    {
        if (obj.text != null)
        {
            StartCoroutine(loadtexture2(obj.text));
        }

    }
    public void LoadImage(string obj)
    {
        if (obj != null)
        {
            StartCoroutine(loadtexture(obj));
        }

    }
    [SerializeField]
    UnityEvent imgsuc,imgfal;
    [SerializeField]
    Sprite default_image;


    IEnumerator loadtexture(string obj)
    {
        Debug.Log(obj);

        //foreach (Image i in sprit)
        //    i.sprite = default_image;
        if (string.IsNullOrEmpty(obj))
        {
            Debug.Log("传回来的Img的格式不是png格式!");
            yield break;
        }

        WWW www = new WWW(obj);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            imgfal.Invoke();
        }
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
            foreach (Image i in sprit)
                i.sprite = sprite;

            imgsuc.Invoke();

        }
    }



    IEnumerator loadtexture2(string obj)
    {
        Debug.Log(obj);

        //foreach (Image i in sprit2)
        //    i.sprite = default_image;

        WWW www = new WWW(obj);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
            imgfal.Invoke();
        }
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
            foreach(Image i in sprit2)
            i.sprite = sprite;

            imgsuc.Invoke();

        }
    }



}
