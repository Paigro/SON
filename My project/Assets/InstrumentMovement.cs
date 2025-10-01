using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentMovement : MonoBehaviour
{
    public float distancia = 10;
    float contador = 0;
    bool vuelta = false;
    // Start is called before the first frame update
    void Start()
    {

    }

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
