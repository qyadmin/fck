using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;

public class PlayVidoeOnGui : MonoBehaviour
{
    [HideInInspector]
    public string Url;

    public VideoPlayer VideoPlayer;

    public Text UrlText;

    //public AudioSource AudioPlayer;

    private string _savePath;

    private FileInfo _file;

    private HttpModel _targetHttpModel;

    private void Update()
    {
        if (VideoPlayer.isPlaying && (ulong)VideoPlayer.frame >= VideoPlayer.frameCount)
        {
            if (_targetHttpModel != null) _targetHttpModel.Get();
            gameObject.SetActive(false);
        }
    }

    public void OnPlayVideoClick()
    {
        var path = Application.persistentDataPath;
        _savePath = path + "/end.mp4";
        Debug.Log(_savePath);
        if (UrlText != null && !string.IsNullOrEmpty(UrlText.text))
        {
            Url = UrlText.text;
        }
        else
            return;
        this.gameObject.SetActive(true);
        _file = new FileInfo(_savePath);
        DirectoryInfo mydir = new DirectoryInfo(_savePath);
        if (File.Exists(_savePath))
        {
            PlayVideo();
        }
        else
        {
            StartCoroutine("DownFile", Url);
        }

    }


    public void SetHttpModel(HttpModel http)
    {
        _targetHttpModel = http;
    }

    private IEnumerator DownFile(string url)
    {
        WWW www = new WWW(url);
        while (!www.isDone && www.error == null)
        {
            yield return null;
        }
        byte[] bytes = www.bytes;
        CreateFile(bytes);
        PlayVideo();
    }

    private void CreateFile(byte[] bytes)
    {
        Stream stream;
        stream = _file.Create();
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log("下载完成");
        stream.Close();
        stream.Dispose();
    }


    private void PlayVideo()
    {
        StartCoroutine(DelayPlayVide(1));
    }

    private IEnumerator DelayPlayVide(float time)
    {
        VideoPlayer.source = VideoSource.Url;
        VideoPlayer.url = _savePath;
        VideoPlayer.Prepare();
        VideoPlayer.playOnAwake = false;
        while (!VideoPlayer.isPrepared)
        {
            yield return null;
        }
        VideoPlayer.Play();
    }
}