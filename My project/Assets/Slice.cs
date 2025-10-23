// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 6 ejercicio 1.
using UnityEngine;

public class SchedEvent : MonoBehaviour
{
    private AudioSource head;
    private AudioSource tail;
    public AudioClip[] pcmDataHeads, pcmDataTails;
    private int nHeads, nTails;
    // Intervalo temporal.
    [SerializeField] private float lap;

    void Awake()
    {
        nHeads = pcmDataHeads.Length;
        nTails = pcmDataTails.Length;
        head = gameObject.AddComponent<AudioSource>();
        tail = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // Heads:
        for (int i = 0; i < nHeads; i++)
        {
            if (pcmDataHeads[i].channels > 1)
            {
                Debug.LogError("NO ES MONO");
                continue;
            }
            int nSamplesLap = (int)(pcmDataHeads[i].frequency * lap);
            var nSamples = pcmDataHeads[i].samples;
            var data = new float[nSamples];
            if (nSamplesLap > nSamples)
            {
                Debug.LogError("EL LAP ES MAYOR QUE EL CLIP");
                continue;
            }
            pcmDataHeads[i].GetData(data, 0);
            applyFadeOut(data, nSamplesLap);
            pcmDataHeads[i].SetData(data, 0);
        }
        // Tails:
        for (int i = 0; i < nTails; i++)
        {
            if (pcmDataHeads[i].channels > 1)
            {
                Debug.LogError("NO ES MONO");
                continue;
            }
            int nSamplesLap = (int)(pcmDataHeads[i].frequency * lap);
            var nSamples = pcmDataHeads[i].samples;
            var data = new float[nSamples];
            if (nSamplesLap > nSamples)
            {
                Debug.LogError("EL LAP ES MAYOR QUE EL CLIP");
                continue;
            }
            pcmDataHeads[i].GetData(data, 0);
            applyFadeIn(data, nSamplesLap);
            pcmDataHeads[i].SetData(data, 0);
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
            int ic = (samples.Length - lapSample) + i;
            samples[ic] = samples[ic] * Mathf.Sqrt((lapSample - t) / lapSample);
            t += lap / lapSample;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int h = Random.Range(0, nHeads), t = Random.Range(0, nTails);
            head.clip = pcmDataHeads[h];
            tail.clip = pcmDataTails[t];

            double clipLength = (double)head.clip.samples / head.pitch;

            int sRATE = AudioSettings.outputSampleRate;
            Debug.Log($"head {h} length {clipLength}  p tail {t}  sRATE: {sRATE}");

            head.Play();
            tail.PlayScheduled(AudioSettings.dspTime + clipLength / sRATE);
        }
    }
}