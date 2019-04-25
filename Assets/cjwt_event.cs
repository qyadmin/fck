using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cjwt_event : MonoBehaviour {

    [SerializeField]
    Transform father;
    [SerializeField]
    Transform other;


    public void resets()
    {
        StopAllCoroutines();
        StartCoroutine(delator());
    }

    IEnumerator delator()
    {
        yield return new WaitForSeconds(0.2f);
        if (other != null && father.childCount != 0)
            other.gameObject.SetActive(true);
        else
            if(other)
            other.gameObject.SetActive(false);

        father.gameObject.SetActive(false);
        father.gameObject.SetActive(true);



    }
}
