using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBackOrder : MonoBehaviour
{

    private GameObject _getGameObject;

    public void Show(GameObject go)
    {
        this.gameObject.SetActive(true);
        _getGameObject = go;
    }


    public void OnSendClick()
    {
        if (_getGameObject == null || _getGameObject.transform.GetComponent<HttpModel>() == null)
            return;
        var http = _getGameObject.transform.GetComponent<HttpModel>();
        http.Get();
    }
}