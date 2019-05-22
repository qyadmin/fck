
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine.UI;

public class PlayGif : MonoBehaviour
{

    public UnityEngine.UI.Image TargetImage;

    public UnityEngine.UI.Image HideImage;

    public GameObject Hint;

    public float Mytime = 0.05f;
    List<Texture2D> zhen;
    Sprite sprite;
    int dex = 0;
    float time;
    bool _isPlay = false;

    private void Awake()
    {
        string path = "";
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            path = string.Format("{0}/StreamingAssets/", Application.dataPath);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            path = string.Format("{0}!/assets/", Application.dataPath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = string.Format("{0}/Raw/", Application.dataPath);
        }
        zhen = GifToTextureByCS(System.Drawing.Image.FromFile(path + "/Open.gif"));
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (_isPlay)
        {
            sprite = Sprite.Create(zhen[dex], new Rect(0, 0, 256, 256), Vector2.zero);
            //Resources.Load<Material>("BiaoQing").mainTexture = zhen[dex];
            TargetImage.sprite = sprite;
            time += Time.deltaTime;
            if (time > Mytime)
            {
                dex++;
                time = 0;
                if (dex == zhen.Count)
                {
                    _isPlay = false;
                    dex = 0;
                    PlaySoundAndClose();
                }
            }
        }
    }


    public void StartPlayGif()
    {
        _isPlay = true;
        TargetImage.gameObject.SetActive(true);
        HideImage.gameObject.SetActive(false);
    }


    private void PlaySoundAndClose()
    {
        var audio = transform.GetComponent<AudioSource>();
        if (audio != null) audio.Play();
        TargetImage.gameObject.SetActive(false);
        Hint.SetActive(true);
        HideImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// gif转换图片
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    List<Texture2D> GifToTextureByCS(System.Drawing.Image target)
    {
        List<Texture2D> texture2D = null;
        if (null != target)
        {
            texture2D = new List<Texture2D>();
            //Debug.LogError(image.FrameDimensionsList.Length);
            //image.FrameDimensionsList.Length = 1;
            //根据指定的唯一标识创建一个提供获取图形框架维度信息的实例;
            FrameDimension frameDimension = new FrameDimension(target.FrameDimensionsList[0]);
            //获取指定维度的帧数;
            int framCount = target.GetFrameCount(frameDimension);
            for (int i = 0; i < framCount; i++)
            {
                //选择由维度和索引指定的帧;
                target.SelectActiveFrame(frameDimension, i);
                var framBitmap = new Bitmap(target.Width, target.Height);
                //从指定的Image 创建新的Graphics,并在指定的位置使用原始物理大小绘制指定的 Image;
                //将当前激活帧的图形绘制到framBitmap上;
                System.Drawing.Graphics.FromImage(framBitmap).DrawImage(target, Point.Empty);
                var frameTexture2D = new Texture2D(framBitmap.Width, framBitmap.Height);
                for (int x = 0; x < framBitmap.Width; x++)
                {
                    for (int y = 0; y < framBitmap.Height; y++)
                    {
                        //获取当前帧图片像素的颜色信息;
                        System.Drawing.Color sourceColor = framBitmap.GetPixel(x, y);
                        //设置Texture2D上对应像素的颜色信息;
                        frameTexture2D.SetPixel(x, framBitmap.Height - 1 - y, new Color32(sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A));
                    }
                }
                frameTexture2D.Apply();
                texture2D.Add(frameTexture2D);
            }
        }
        return texture2D;
    }
}