// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 5 ejercicio 1.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispersor : MonoBehaviour
{
    [SerializeField] private int polifonia = 5; // Numero de Audio Sources para crear.
    [Range(0f, 30f)] public float minTime = 5, maxTime = 10; // Interavalo de lanzamiento.
    [Range(0f, 1f)] public float minVol = 0.2f, maxVol = 0.8f; // Volumen minimo y mximo del lanmiento.
    [Range(-1f, 1f)] public float minPan = -0.5f, maxPan = 0.5f; // Paneo aleatorio del lanzamiento.
    [SerializeField] private float pitchVar = 0.05f; // Variacion en el pitch.

    [SerializeField] private string ruta; // Ruta para carga los audios.

    public AudioClip[] audioClips; // Lista de clips de audio cargados de resources.
    public List<AudioSource> canales; // Lista de los canales de audio.


    private void Start()
    {
        // Crear los Audio Sources segun la polifonia.
        canales = new List<AudioSource>(polifonia);
        for (int i = 0; i < polifonia; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            canales.Add(source);
        }
        // Cargar los audios de resources.
        audioClips = Resources.LoadAll<AudioClip>(ruta);
        // Inicializamos todos los canales de audio.
        for (int i = 0; i < polifonia; i++)
        {
            PlaySound();
        }
    }

    IEnumerator Waitforit(AudioSource audioSource)
    {
        // Tiempo de espera aleatorio en el intervalo [minTime,maxTime].
        float waitTime = Random.Range(minTime, maxTime);

        // Miramos si hay un clip asignado al source (sirve para la primera vez q se ejecuta)
        if (audioSource.clip == null)
            // Waitfor seconds suspende la coroutine durante waitTime.
            yield return new WaitForSeconds(waitTime);

        // Cuando hay clip se mete la longitud del clip + el tiempo de espera para esperar entre lanzamientos.
        else
            yield return new WaitForSeconds(audioSource.clip.length + waitTime);

        // Si esta activado reproducimos sonido.
        PlaySound();
    }

    void PlaySound()
    {
        int i = 0;
        bool found = false;
        while (i < canales.Count && !found)
        {
            if (!canales[i].isPlaying)
            {
                found = true;
                SetSourceProperties(canales[i], audioClips[Random.Range(0, audioClips.Length)]);
                canales[i].Play();
                StartCoroutine(Waitforit(canales[i]));
            }
            i++;
        }
    }

    public void SetSourceProperties(AudioSource audioSource, AudioClip audioData)
    {
        audioSource.loop = false; // No queremos loop.
        audioSource.clip = audioData; // Le ponemos el clip al audioSource.
        audioSource.volume = Random.Range(minVol, maxVol); // Le ponemos el volumen entre el rango.
        audioSource.panStereo = Random.Range(minPan, maxPan); // Lo mismo con el paneo.
        audioSource.pitch = Random.Range(1 - pitchVar, 1 + pitchVar); // Lo mismo con el pitch.
    }

    public void SetStats(string path, int poly, float minT, float maxT, float minV, float maxV, float minP, float maxP, float pitchV)
    {
        ruta = path;
        polifonia = poly;
        minTime = minT; maxTime = maxT;
        minVol = minV; maxVol = maxV;
        minPan = minP; maxPan = maxP;
        pitchVar = pitchV;
    }
}
