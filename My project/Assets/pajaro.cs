using UnityEngine;

public class pajaro : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}