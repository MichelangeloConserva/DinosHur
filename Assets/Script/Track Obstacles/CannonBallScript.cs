using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{

    public Rigidbody rb;
    public float livingTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObject(livingTime));
    }


    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("cannonball"))
        {
            StartCoroutine(DestroyObject(1f));
        }
    }

    private IEnumerator DestroyObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
