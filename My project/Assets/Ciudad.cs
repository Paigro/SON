// Andres Garcia Navarro.
// Pablo Iglesias Rodrigo.
// Hoja 5 ejercicio 2.

using UnityEngine;


public class Ciudad : MonoBehaviour
{
    [Range(0f, 1f)] public float iTraffic; // Intensidad del trafico.
    [Range(0f, 1f)] public float iChatter; // Intensidad de las voces.

    // Rutas de los archivos de audio.
    [SerializeField] private string trafficPadPath = "Sounds/Hoja5/TrafficPad/traffic_pad";
    [SerializeField] private string passingPath = "Sounds/Hoja5/Passing";
    [SerializeField] private string trainPath = "Sounds/Hoja5/Train";
    [SerializeField] private string hornPath = "Sounds/Hoja5/Horn";
    [SerializeField] private string sirenPath = "Sounds/Hoja5/Siren";
    [SerializeField] private string chatterPadPath = "Sounds/Hoja5/ChatterPad/chatter_pad";
    [SerializeField] private string chatterPath = "Sounds/Hoja5/Chatter";


    [Range(0f, 30f)] public float defaultMinTime = 5, defaultMaxTime = 10; // Tiempos minimos y maximos de cada capa por defecto.
    [Range(0f, 1f)] public float defaultMinVol = 0.2f, defaultMaxVol = 0.8f; // Volumentes minimos y maximos de cada capa por defecto.
    [Range(-1f, 1f)] public float defaultMinPan = -0.5f, defaultMaxPan = 0.5f; // Paneos minimos y maximos de cada capa por defecto.
    [SerializeField] private float defaultPitchVar = 0.05f; // Pitch por defecto de las capas.


    private Dispersor passingDisp;
    private Dispersor trainDisp;
    private Dispersor hornDisp;
    private Dispersor sirenDisp;
    private Dispersor chatterDisp;
    private AudioSource trafficPad, chatterPad;

    private void Start()
    {
        // El traffic pad primero, que esta de fondo siempre con volumen iTraffic..
        trafficPad = gameObject.AddComponent<AudioSource>();
        trafficPad.clip = Resources.Load<AudioClip>(trafficPadPath);
        trafficPad.loop = true;
        trafficPad.volume = iTraffic;
        trafficPad.Play();

        // Probabilidades contando con el valor de iTraffic:
        //------Con 0.2.
        if (iTraffic >= 0.2f)
        {
            // Los coches pasando.
            passingDisp = ConfigureDispersor(passingPath, 5, // Ruta de la carpeta y polifonia.
                defaultMinTime - 0.75f * iTraffic, defaultMaxTime - 0.75f * iTraffic, // Cambio de lso tiempos en base al iTraffic.
                defaultMinVol + 0.05f * iTraffic, defaultMaxVol + 0.05f * iTraffic, // Cambio de los volumenes en base al iTraffic.
                defaultMinPan, defaultMaxPan, // Paneo.
                defaultPitchVar); // Pitch.

            // Los trenes pasando.
            trainDisp = ConfigureDispersor(trainPath, 2, // Ruta de la carpeta y polifonia.
                defaultMinTime + 0.75f * iTraffic, defaultMaxTime + 0.75f * iTraffic, // Cambio de lso tiempos en base al iTraffic.
                defaultMinVol + 0.05f * iTraffic, defaultMaxVol + 0.05f * iTraffic, // Cambio de los volumenes en base al iTraffic.
                defaultMinPan, defaultMaxPan, // Paneo.
                defaultPitchVar); // Pitch.
        }
        //------Con 0.5.
        if (iTraffic >= 0.5f)
        {
            // Los horns.
            hornDisp = ConfigureDispersor(hornPath, 3, // Ruta de la carpeta y polifonia.
                defaultMinTime - 0.75f * iTraffic, defaultMaxTime - 0.75f * iTraffic, // Cambio de lso tiempos en base al iTraffic.
                defaultMinVol, defaultMaxVol, // Cambio de los volumenes en base al iTraffic.
                defaultMinPan, defaultMaxPan, // Paneo.
                defaultPitchVar); // Pitch.

            // La sirena.
            sirenDisp = ConfigureDispersor(sirenPath, 1, // Ruta de la carpeta y polifonia.
                defaultMinTime - 0.75f * iTraffic, defaultMaxTime - 0.75f * iTraffic, // Cambio de lso tiempos en base al iTraffic.
                defaultMinVol, defaultMaxVol, // Cambio de los volumenes en base al iTraffic.
                defaultMinPan, defaultMaxPan, // Paneo.
                defaultPitchVar); // Pitch.
        }


        // El cahtter pad primero, que esta de fondo siempre con volumen iChatter.
        chatterPad = gameObject.AddComponent<AudioSource>();
        chatterPad.clip = Resources.Load<AudioClip>(chatterPadPath);
        chatterPad.loop = true;
        chatterPad.volume = iChatter;
        chatterPad.Play();

        // Probabilidades contando con el valor de iChatter:
        //------Con 0.5.
        if (iChatter >= 0.5f)
        {
            // Las voces sonando.
            chatterDisp = ConfigureDispersor(chatterPath, 6, // Ruta de la carpeta y polifonia.
                defaultMinTime - 0.75f * iChatter, defaultMaxTime - 0.75f * iChatter, // Cambio de lso tiempos en base al iTraffic.
                defaultMinVol + 0.05f * iChatter, defaultMaxVol + 0.05f * iChatter, // Cambio de los volumenes en base al iTraffic.
                defaultMinPan, defaultMaxPan, // Paneo.
                defaultPitchVar); // Pitch.
        }
    }


    private Dispersor ConfigureDispersor(string path, int poly, float minTim, float maxTim, float minVol, float maxVol, float minPan, float maxPan, float pitchVar)
    {
        GameObject gameObj = new GameObject("Dispersor: " + path);
        gameObj.transform.parent = this.transform;
        Dispersor newDisperor = gameObj.AddComponent<Dispersor>();
        newDisperor.SetStats(path, poly, minTim, maxTim, minVol, maxVol, minPan, maxPan, pitchVar);

        return newDisperor;
    }
}