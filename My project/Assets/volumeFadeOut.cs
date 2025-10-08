// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 4 ejercicio 2.

using UnityEngine;


public class volumeFadeOut : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    [SerializeField] float intervalTime = 2;
    private bool isFadingOut;
    private float volumeMod;

    // Start is called before the first frame update
    void Start()
    {
        volumeMod = 1 / intervalTime;
    }

    // Update is called once per frame
    void Update()
    {
        // Programacion defensiva.
        if (audio == null)
        {
            return;
        }
        // INPUT:
        // Para que haga el fade out.
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (audio.volume > 0)
            {
                Debug.Log("Strt.");
                isFadingOut = true;
            }
        }
        // Para 
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            audio.volume = 1;
        }

        if (isFadingOut)
        {
            //Debug.Log("fding.");
            //Debug.Log(volumeMod);
            if (audio.volume <= 0)
            {
                Debug.Log("End.");
                isFadingOut = false;
            }
            else
            {
                audio.volume -= volumeMod * Time.deltaTime;
            }
        }
    }
}
