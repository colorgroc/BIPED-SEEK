using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class TextFadeIn_Out : MonoBehaviour
{
    float time;
    private float duration = 1f;
    bool change;
    private void Start()
    {
        Time.timeScale = 1;
        time = 0;
    }
    private void Update()
    {
        time += Time.deltaTime;

        if (time >= duration)
        {
            change = !change;
            time = 0;
        }

        if (change)
        {
            GetComponent<Text>().CrossFadeAlpha(0.0f, duration, true);

        }
        else if (!change)
        {
            // and back over 500ms.
            GetComponent<Text>().CrossFadeAlpha(1.0f, duration, true);
        }
          
        
    }
}
