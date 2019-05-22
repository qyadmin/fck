
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGif : MonoBehaviour
{

    public GameObject TargetImage;

    public GameObject HideImage;

    public GameObject Hint;

    public HttpModel Refresh;

    private Animator _anim;

    bool _isPlay = false;

    void Start()
    {
        _anim = TargetImage.transform.GetComponent<Animator>();
    }

    void Update()
    {
        var info = _anim.GetCurrentAnimatorStateInfo(0);
        if (_isPlay && info.normalizedTime >= 1.0f)
        {
            PlaySoundAndClose();
            
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
        _isPlay = false;
        Hint.SetActive(true);
        Refresh.Get();
        HideImage.gameObject.SetActive(true);
        TargetImage.gameObject.SetActive(false);
    }
}