using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : IObstacle
{

    public GameObject area;
    public float explosionForce = 10f;
    public float explosionRadius = 7.5f;
    public float upForce = 0.5f;

    public float delayTime = 1f;
    private bool ready = true;

    public override void Activate()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upForce, ForceMode.Impulse);
            }

            if (collider.Equals(LevelController.Instance.PlayerController.ObstacleCollider))
            {
                LevelController.Instance.PlayerController.DecreaseHealth();
            }
        }

        StartCoroutine(Ready(Cooldown));
        LevelController.Instance.PlaySound(SoundType.Explosion, transform.position);

    }

    // Start is called before the first frame update


    public void OnTriggerEnter(Collider other)
    {
        if (ready == true)
        {
            Invoke(nameof(Activate), 0.2f);
            area.SetActive(false);
            ready = false;
        }
    }

    private IEnumerator Ready(float time)
    {
        yield return new WaitForSeconds(time);
        area.SetActive(true);
        ready = true;
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
