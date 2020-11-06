using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rigidBody;
    // Start is called before the first frame update
    public float bulletSpeed = 50f;
    public float aliveTime = 5f;

    public void Start()
    {
       StartCoroutine(DestroySelf(aliveTime));
    }
    public void SetVelocity(Vector3 direction, Vector3 initialVelocity)
    {
        rigidBody.velocity = direction * bulletSpeed + initialVelocity;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f, other.transform.position,5f,100f);
            Destroy(gameObject);
            Destroy(other.transform.parent.gameObject, 1f);
        }
        
    }

    public IEnumerator DestroySelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    


}
