using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SildeShowScripts : MonoBehaviour
{

    public TweenPosition[] ShowImages;

    public Transform StartPositions;
    public Transform MiddlePositions;
    public Transform Endositions;

    public Transform Content;

    public HttpModel Http;


    private int _imageNum = 0;


    private bool _isStop = true;

    private List<Sprite> _imageSprites;

    private float _stopTime = 5f;

    private float _stopDeltaTime = 0;


    void Start()
    {
        _imageSprites = new List<Sprite>();
        Http.Get();
    }

    void Update()
    {
        if (_imageSprites.Count >= 2)
        {
            if (_isStop)
                _stopDeltaTime += Time.deltaTime;
            if (_stopDeltaTime >= _stopTime)
            {
                ShowImages[0].PostionFrom = StartPositions.localPosition;
                ShowImages[0].PostionTo = MiddlePositions.localPosition;
                ShowImages[0].transform.GetComponent<Image>().sprite = _imageSprites[GetNum(_imageNum + 1)];
                ShowImages[0].InitAnimation();

                ShowImages[1].PostionFrom = MiddlePositions.localPosition;
                ShowImages[1].PostionTo = Endositions.localPosition;
                _imageNum = GetNum(_imageNum);
                ShowImages[1].transform.GetComponent<Image>().sprite = _imageSprites[_imageNum];
                ShowImages[1].InitAnimation();

                _imageNum++;
                _isStop = false;
                _stopDeltaTime = 0;
            }
        }
    }

    public void StartMoveEvent()
    {
        _isStop = true;
        ShowImages[0].transform.localPosition = StartPositions.localPosition;
        ShowImages[1].transform.localPosition = MiddlePositions.localPosition;
        ShowImages[1].transform.GetComponent<Image>().sprite = _imageSprites[GetNum(_imageNum)];
    }

    private int GetNum(int num)
    {
        return num % _imageSprites.Count;
    }

    public void LoadImageAndShow()
    {
        var imageTexts = Content.GetComponentsInChildren<Text>();
        foreach (var image in imageTexts)
        {
            StartCoroutine("LoadImage", image.text);
        }
    }

    IEnumerator LoadImage(string path)
    {
        Debug.Log(path);
        WWW www = new WWW(path);

        yield return www;

        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
            _imageSprites.Add(sprite);
            ShowImages[1].transform.GetComponent<Image>().sprite = _imageSprites[0];
        }
        else
            Debug.Log(www.error);
    }
}