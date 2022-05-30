using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    public int ballForce;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < -4)
        {
            Destroy(gameObject);
        }    
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Magnet") && !PlayerController.Instance._canMove)
        {
            _rb.AddForce(0, 0, ballForce);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlatformElevator"))
        {
            StartCoroutine(DestroyBalls());
        }
    }
    public IEnumerator DestroyBalls()
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
