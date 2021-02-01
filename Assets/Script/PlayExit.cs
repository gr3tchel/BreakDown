using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
       if ( Input.GetButtonDown("Cancel"))
            {
            Application.Quit();
            
             }

        if (Input.GetButtonDown("Jump")&& GetComponent<CanvasGroup>().alpha == 1)
        {
            Time.timeScale = 1;
            //close canvas
            GetComponent<CanvasGroup>().alpha=0;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }
}
