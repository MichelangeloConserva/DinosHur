using System.Collections;
using UnityEngine;


public enum BumperState { INACTIVE = 0, ACTIVE, RETRACTING};
public class BumperObstacle : IObstacle
{
    // Start is called before the first frame update
    public Rigidbody rb;

    public float speed = 10;
    public float retractionFactor = 0.5f;

    private BumperState bumperState = BumperState.INACTIVE;


    private Vector3 startPosition;
    private Vector3 endPosition;
    
    private Vector3 direction;

    private float distance;
    private float currentDistance;

    private float startTime;

    

    public new void Start()
    {
        base.Start();
        startPosition = transform.GetChild(0).position;
        endPosition = transform.GetChild(1).position;

        distance = Vector3.Distance(startPosition, endPosition);
    }
    public void Update()
    {

        currentDistance = Vector3.Distance(transform.position, startPosition);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }

        // check if bumper should stop
        if (currentDistance > distance && bumperState == BumperState.ACTIVE)
        {
            direction = Vector3.Normalize(startPosition - transform.position);
            rb.velocity = direction * speed * retractionFactor;
            float retractingTime = (Time.time - startTime) / retractionFactor;

            StartCoroutine(Reset(retractingTime));

            bumperState = BumperState.RETRACTING;
        }
    }

    public override void Activate()
    {
        if (bumperState == BumperState.INACTIVE)
        {
            direction = Vector3.Normalize(endPosition - startPosition);
            rb.velocity = direction * speed;
            
            startTime = Time.time;
            bumperState = BumperState.ACTIVE;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        LevelController.Instance.PlaySound(SoundType.Bumper, transform.position);
    }

    private IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = startPosition;
        bumperState = BumperState.INACTIVE;
    }

}
