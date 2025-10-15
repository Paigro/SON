// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 5 ejercicio 1.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispersor : MonoBehaviour
{
    [SerializeField] private int polifonia; // Numero de Audio Sources para crear.
    [Range(0f, 30f)] public float minTime, maxTime; // Interavalo de lanzamiento.
    [Range(0f, 1f)] public float minVol, maxVol; // Volumen minimo y mximo de lnmiento.
    [SerializeField] private float pitchVar;
    [SerializeField] private string ruta;

    public AudioClip[] audioClips;
    public List<AudioSource> channels;

    private void Awake()
    {
        // Crear los Audio Sources.
        channels = new List<AudioSource>(polifonia);
        for (int i = 0; i < polifonia; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            channels.Add(source);
        }
        // Cargar los audios de resources.
        audioClips = Resources.LoadAll<AudioClip>("Sounds/ciudad");
    }

    private void Start()
    {
        for (int i = 0; i < polifonia; i++)
        {
            PlaySound();
        }
    }
    
    IEnumerator Waitforit(AudioSource audioSource)
    {
        // tiempo de espera aleatorio en el intervalo [minTime,maxTime]
        float waitTime = Random.Range(minTime, maxTime);
        Debug.Log(waitTime);

        // miramos si hay un clip asignado al source (sirve para la primera vez q se ejecuta)
        if (audioSource.clip == null)
            // waitfor seconds suspende la coroutine durante waitTime
            yield return new WaitForSeconds(waitTime);

        // cuando hay clip se añade la long del clip + el tiempo de espera para esperar entre lanzamientos
        else
            yield return new WaitForSeconds(audioSource.clip.length + waitTime);

        // si esta activado reproducimos sonido
        PlaySound();
    }

    void PlaySound()
    {
        int i = 0;
        bool fount = false;
        while (i < channels.Count && !fount)
        {
            if (!channels[i].isPlaying)
            {
                fount = true;
                SetSourceProperties(channels[i], audioClips[Random.Range(0, audioClips.Length)]);
                channels[i].Play();
                Debug.Log("back in it");
                StartCoroutine(Waitforit(channels[i]));
                break;
            }
            i++;
        }
    }

    public void SetSourceProperties(AudioSource audioSource, AudioClip audioData)
    {
        audioSource.loop = false;
        audioSource.clip = audioData;
        audioSource.volume = Random.Range(minVol, maxVol);
        audioSource.pitch = Random.Range(1 - pitchVar, 1 + pitchVar);
    }
}
