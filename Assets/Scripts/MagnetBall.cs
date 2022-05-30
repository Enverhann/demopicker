using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBall : MonoBehaviour
{
    [SerializeField] private float magneticPull;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * magneticPull);
        }
    }
}
