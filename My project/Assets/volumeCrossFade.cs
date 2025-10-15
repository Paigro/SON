// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 4 ejercicio 3.

using System.Collections;
using UnityEngine;

public class volumeCrossFade : MonoBehaviour
{
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioSource auxAudioSource;

    [SerializeField] AudioClip[] audioClips;

    [SerializeField] float lap = 6;

    private bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        // Lap en rango.
        if (lap < 0.1f)
        {
            lap = 0.1f;
        }
        else if (lap > 6f)
        {
            lap = 6f;
        }
        // Pone un clip aleatorio.
        mainAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        auxAudioSource.volume = 0;
        mainAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !isFading)
        {
            StartCoroutine(CrossFade());
            isFading = true;
        }
    }

    IEnumerator CrossFade()
    {
        float laps = 1 / lap;
        auxAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        auxAudioSource.volume = 0;
        auxAudioSource.Play();
        for (float t = 0; t < lap; t += laps)
        {
            mainAudioSource.volume = Mathf.Lerp(1, 0, 1 - Mathf.Sqrt((lap - t) / lap)); // Fade out.
            auxAudioSource.volume = Mathf.Lerp(0, 1, Mathf.Sqrt(t / lap)); // Fade in.
            yield return new WaitForSecondsRealtime(laps);
        }
        // Poner la nueva pista en el main Audio Source y configurar el resto de cosas.
        mainAudioSource.clip = auxAudioSource.clip;
        mainAudioSource.Play();
        mainAudioSource.timeSamples = auxAudioSource.timeSamples;
        auxAudioSource.volume = 0;
        mainAudioSource.volume = 1;
        isFading = false;
    }
}