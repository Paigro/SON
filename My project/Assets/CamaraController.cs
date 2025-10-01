using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public int vel = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            transform.position = transform.position + (transform.forward * vel);
        if (Input.GetKeyDown(KeyCode.S))
            transform.position = transform.position + (transform.forward * -vel);
        if (Input.GetKeyDown(KeyCode.A))
            transform.Rotate(0, -5, 0);
        if (Input.GetKeyDown(KeyCode.D))
            transform.Rotate(0, 5, 0);
    }
}