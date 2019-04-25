using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConversionRatio : MonoBehaviour
{

    [SerializeField]
    InputField a;
    [SerializeField]
    private float bili = 1;
    private InputField current;
    void Start()
    {
        current = GetComponent<InputField>();
        a.onEndEdit.AddListener(delegate(string data) 
        {
            if(current==null)
                current = GetComponent<InputField>();
            current.text = (Mathf.Floor(int.Parse(data)*bili)).ToString();
            
        }); 
    }

}
