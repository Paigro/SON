
// '1' empieza la reproducción del sonido intermitente
// '2' lo para

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntermitentSound : MonoBehaviour {
    [SerializeField]
    private AudioSource _Speaker01;  // audio source asosicada a la entidad
    [Range(0f, 1f )]
    public float minVol, maxVol, SourceVol;  // volumenes máximo y mínimo establecidos y volumen origintal del source
    [Range(0f, 30f )]
    public float minTime, maxTime;  // intervalo temporal de lanzamiento
    [Range(0, 50)]
    public int distRand, maxDist;   // 
    [Range(0f, 1.1f )]
    public float spatialBlend;
    public AudioClip[] pcmData;
    public bool enablePlayMode;

    void Awake(){
        _Speaker01 = GetComponent<AudioSource>();
        if (_Speaker01 == null) _Speaker01 = gameObject.AddComponent<AudioSource>();        
    }

    void Start() {
        _Speaker01.playOnAwake = false;
        _Speaker01.loop = false;
        _Speaker01.volume = 0.1f;
    }

    // Update is called once per frame
    void Update() {
        if (!enablePlayMode){
            Debug.Log("NotPlaying");
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                enablePlayMode = true;
                StartCoroutine("Waitforit");
            }
        } else if (enablePlayMode) 
            if (Input.GetKeyDown(KeyCode.Alpha2)) StopSound();            
    }



    IEnumerator Waitforit() {
        // tiempo de espera aleatorio en el intervalo [minTime,maxTime]
        float waitTime = Random.Range(minTime, maxTime);
        Debug.Log(waitTime);

        // miramos si hay un clip asignado al source (sirve para la primera vez q se ejecuta)
        if (_Speaker01.clip == null)     
            // waitfor seconds suspende la coroutine durante waitTime
            yield return new WaitForSeconds(waitTime);        
        
        // cuando hay clip se añade la long del clip + el tiempo de espera para esperar entre lanzamientos
        else         
            yield return new WaitForSeconds(_Speaker01.clip.length + waitTime);        
        
        // si esta activado reproducimos sonido
        if (enablePlayMode) PlaySound();        
    }

    void PlaySound() {
        SetSourceProperties(pcmData[Random.Range(0, pcmData.Length)], minVol, maxVol, distRand, maxDist, spatialBlend);
        _Speaker01.Play();
        Debug.Log("back in it");
        StartCoroutine("Waitforit");
    }



    public void SetSourceProperties(AudioClip audioData, float minVol, float maxVol,
                                    int minDist, int maxDist, float SpatialBlend) {
        _Speaker01.loop = false;
        _Speaker01.maxDistance = maxDist - Random.Range(0f, distRand);
        _Speaker01.spatialBlend = spatialBlend;
        _Speaker01.clip = audioData;
        _Speaker01.volume = SourceVol + Random.Range(minVol, maxVol);
    }




    void StopSound() {
        enablePlayMode = false;
        Debug.Log("stop");
    }
}    
    
