using UnityEngine;
using TMPro;
using System;

public class Stopwatch : MonoBehaviour
{
    // variable for current time
    float currentTime;

    // reference to text
    public TextMeshProUGUI currentTimeText;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // update current time and update the text on the canvas
        currentTime = currentTime + Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }
}
