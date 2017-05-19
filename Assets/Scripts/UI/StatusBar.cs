using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{

    public static int count = 0;
    private TextMesh text;
    
    void Start()
    {
        text = GetComponent<TextMesh>();
        count = 0;
    }

    void Update()
    {
        text.text = count.ToString();
    }

}
