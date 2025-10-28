// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 6 ejercicio 1.
using UnityEngine;

public class SchedEvent : MonoBehaviour
{
    private AudioSource head; // Para reproducir los heads.
    private AudioSource tail; // Para reproducir los tail.
    private AudioSource casing; // Para reproducir los casing.

    public AudioClip[] pcmDataHeads; // Clips de audio de heads.
    public AudioClip[] pcmDataTails; // Clips de audio de tails.
    public AudioClip[] pcmDataCasing; // Clips de audio de casing.

    private int nHeads; // Numero de heads.
    private int nTails; // Numero de tails.
    private int nCasing; // Numero de casing.

    [SerializeField] private float lap; // Intervalo temporal.

    void Awake()
    {
        nHeads = pcmDataHeads.Length;
        nTails = pcmDataTails.Length;
        nCasing = pcmDataCasing.Length;
        head = gameObject.AddComponent<AudioSource>();
        tail = gameObject.AddComponent<AudioSource>();
        casing = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // Heads:
        for (int i = 0; i < nHeads; i++)
        {
            // Error si el audio no es MONO.
            if (pcmDataHeads[i].channels > 1)
            {
                Debug.LogError("HEAD NO ES MONO");
                continue;
            }

            int nSamples = pcmDataHeads[i].samples;
            int nSamplesLap = (int)(pcmDataHeads[i].frequency * lap);

            // Error si la duracion del lap es mayor a la del clip.
            if (nSamplesLap > nSamples)
            {
                Debug.LogError("EL LAP DE HEAD ES MAYOR QUE EL CLIP");
                continue;
            }
            int offset = (nSamples - nSamplesLap);
            float[] data = new float[nSamplesLap];
            pcmDataHeads[i].GetData(data, offset); // Cogemos los samples.
            applyFadeOut(data, nSamplesLap); // Les aplicamos el FadeOut.
            pcmDataHeads[i].SetData(data, offset); // Ponemos los nuevos samples en el clip.
        }
        // Tails:
        for (int i = 0; i < nTails; i++)
        {
            // Error si el audio no es MONO.
            if (pcmDataTails[i].channels > 1)
            {
                Debug.LogError("TAIL NO ES MONO");
                continue;
            }

            int nSamples = pcmDataTails[i].samples;
            int nSamplesLap = (int)(pcmDataTails[i].frequency * lap);
            float[] data = new float[nSamples];

            // Error si la duracion del lap es mayor a la del clip.
            if (nSamplesLap > nSamples)
            {
                Debug.LogError("EL LAP DE TAILES MAYOR QUE EL CLIP");
                continue;
            }
            pcmDataTails[i].GetData(data, 0); // Cogemos los samples.
            applyFadeIn(data, nSamplesLap); // Les aplicamos el FadeIn.
            pcmDataTails[i].SetData(data, 0); // Ponemos los nuevos samples en el clip.
        }
    }
    private void applyFadeIn(float[] samples, int lapSample)
    {
        float t = 0;
        for (int i = 0; i < lapSample; i++)
        {
            int ic = (samples.Length - lapSample) + i;
            samples[ic] = samples[ic] * Mathf.Sqrt(t / lapSample);
            t += lap / lapSample;
        }
    }

    private void applyFadeOut(float[] samples, int lapSample)
    {
        float t = 0;
        for (int i = 0; i < lapSample; i++)
        {
            samples[i] = samples[i] * Mathf.Sqrt((lapSample - t) / lapSample);
            t += lap / lapSample;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int h = Random.Range(0, nHeads); // Random head.
            int t = Random.Range(0, nTails); // Random tail.
            int c = Random.Range(0, nCasing); // Random casing.

            head.clip = pcmDataHeads[h];
            tail.clip = pcmDataTails[t];
            casing.clip = pcmDataCasing[c];

            double headLength = (double)head.clip.samples / head.clip.frequency;
            double tailLength = (double)tail.clip.samples / head.clip.frequency;

            int sRATE = AudioSettings.outputSampleRate;
            Debug.Log($"head {h} length {headLength}  p tail {t}  sRATE: {sRATE}");

            head.Play();
            tail.PlayScheduled(AudioSettings.dspTime + headLength - lap);
            casing.PlayScheduled(AudioSettings.dspTime + headLength + tailLength);
        }
    }
}