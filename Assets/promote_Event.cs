using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class promote_Event : MonoBehaviour {

    [SerializeField]
    Transform name;

    public void valueReset()
    {
        StartCoroutine(delate());
    }

    IEnumerator delate()
    {
        yield return new WaitForSeconds(0.2f);
        name.gameObject.SetActive(false);
        name.gameObject.SetActive(true);
    }
}
