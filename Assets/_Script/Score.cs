using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<TMP_Text>();
        text.text = PlayerPrefs.GetInt("Score").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
