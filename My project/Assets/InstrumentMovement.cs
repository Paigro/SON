using UnityEngine;

public class InstrumentMovement : MonoBehaviour
{
    [SerializeField] private float distancia = 10;
    [SerializeField] private float contador = 0;
    [SerializeField] private bool vuelta = false;

    // Update is called once per frame
    void Update()
    {
        if (vuelta)
        {
            transform.position += new Vector3(0.1f, 0, 0);
            contador += Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-0.1f, 0, 0);
            contador += Time.deltaTime;
        }
        if (contador >= distancia)
        {
            vuelta = !vuelta;
            contador = 0;
        }
    }
}