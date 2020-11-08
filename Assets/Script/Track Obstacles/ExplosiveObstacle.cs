using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour, IObstacle
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float upForce = 0.5f;

    public AudioClip audioClip;

    public void Activate()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upForce, ForceMode.Impulse);
                
            }
        }

        LevelController.Instance.PlaySound(SoundType.Explosion, transform.position);
        Destroy(gameObject);
       
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Activate();
        }
    }
}
