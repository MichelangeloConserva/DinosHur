using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallScript : MonoBehaviour
{

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObject(10));
    }


    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
