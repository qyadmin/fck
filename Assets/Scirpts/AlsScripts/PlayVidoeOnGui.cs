using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.IO;

public class PlayVidoeOnGui : MonoBehaviour
{
    [HideInInspector]
    public string UrlPath;

    public VideoPlayer VideoPlayer;

    //public AudioSource AudioPlayer;

    private string _savePath;

    private bool _down;

    private FileInfo _file;

    private void Update()
    {
        if ((ulong)VideoPlayer.frame >= VideoPlayer.frameCount)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPlayVideoClick()
    {
        this.gameObject.SetActive(true);
        var path = Application.persistentDataPath;
        //Debug.Log(path);
        _savePath = path + "/video.mp4";
        _file = new FileInfo(_savePath);
        DirectoryInfo mydir = new DirectoryInfo(_savePath);
        if (File.Exists(_savePath))
        {
            PlayVideo();
        }
        else
        {
            StartCoroutine(DownFile(UrlPath));
        }
    }

    private IEnumerator DownFile(string url)
    {
        WWW www = new WWW(url);
        _down = false;
        yield return www;
        _down = true;
        if (www.isDone)
        {
            byte[] bytes = www.bytes;
            CreateFile(bytes);
            PlayVideo();
        }
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