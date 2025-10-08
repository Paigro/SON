using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeMod : MonoBehaviour
{

    [SerializeField] AudioSource audio;
    [SerializeField] float volumeMod = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (audio.volume > 0)
            {
                audio.volume -= volumeMod;
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (audio.volume < 1)
            {
                audio.volume += volumeMod;
            }
        }

    }
}
