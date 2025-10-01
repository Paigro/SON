using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pajaroGenerator : MonoBehaviour
{
    public GameObject pajaroPrefab;
    public int ladoDelCuadradoSpawner = 10;
    GameObject pajaro;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pajaro == null)
        {
            pajaro = Instantiate(pajaroPrefab);
            pajaro.transform.position = new Vector3(Random.Range(-ladoDelCuadradoSpawner, ladoDelCuadradoSpawner), 0.2f, Random.Range(-ladoDelCuadradoSpawner, ladoDelCuadradoSpawner));
        }
    }
    
}