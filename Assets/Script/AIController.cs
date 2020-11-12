using System.Linq;
using UnityEngine;

using static Utils;

public class AIController : MonoBehaviour
{

    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        DrawBox_(new Box(origin, halfExtents, orientation), color);
    }
    public static void DrawBox_(Box box, Color color)
    {
        Debug.DrawLine(box.frontTopLeft, box.frontTopRight, color);
        Debug.DrawLine(box.frontTopRight, box.frontBottomRight, color);
        Debug.DrawLine(box.frontBottomRight, box.frontBottomLeft, color);
        Debug.DrawLine(box.frontBottomLeft, box.frontTopLeft, color);

        Debug.DrawLine(box.backTopLeft, box.backTopRight, color);
        Debug.DrawLine(box.backTopRight, box.backBottomRight, color);
        Debug.DrawLine(box.backBottomRight, box.backBottomLeft, color);
        Debug.DrawLine(box.backBottomLeft, box.backTopLeft, color);

        Debug.DrawLine(box.frontTopLeft, box.backTopLeft, color);
        Debug.DrawLine(box.frontTopRight, box.backTopRight, color);
        Debug.DrawLine(box.frontBottomRight, box.backBottomRight, color);
        Debug.DrawLine(box.frontBottomLeft, box.backBottomLeft, color);
    }


    public float slow = 0.5f;
    public float turner = 2f;
    public float minTurnToTurn = 0.01f;

    private AStarController asc;
    private KartGame.KartSystems.ArcadeKart ak;

    //Start is called before the first frame update
    void Start()
    {
        ak = GetComponent<KartGame.KartSystems.ArcadeKart>();
        asc = GetComponent<AStarController>();
    }

    private Vector2 BasicAI()
    {
        if (Time.time < 1)
            return new Vector2(0, 0);
        return new Vector2(UnityEngine.Random.Range(-0.4f, 0.4f), 1);
    }


    private float AngleToTurn()
    {

        var rb = GetComponent<Rigidbody>().velocity;

        if (rb.magnitude > 2)
        {

            var ch = transform.parent.GetChild(0).transform.forward;

            var heading = asc.curTargetPos() - transform.position;
            var h = new Vector3(heading.x, 0, heading.z).normalized;
            var z = Vector3.Lerp(new Vector3(rb.x, 0, rb.z).normalized, new Vector3(ch.x, 0, ch.z).normalized, 0.5f);

            var cross = Vector3.Cross(z, h);

            return Mathf.Clamp(cross.y * turner, -1, 1);
        }
        else
        {
            var heading = asc.curTargetPos() - transform.position;
            var cross = Vector3.Cross(transform.forward, heading.normalized);
            return Mathf.Clamp(cross.y * turner, -1, 1);
        }

        //var rb = GetComponent<Rigidbody>().velocity;

        //if (rb.magnitude > 2)
        //{

        //    var ch = transform.parent.GetChild(0).transform.forward;

        //    var heading = asc.curPath.Last() - transform.position;
        //    var h = new Vector3(heading.x, 0, heading.z).normalized;
        //    var z = Vector3.Lerp(new Vector3(rb.x, 0, rb.z).normalized, new Vector3(ch.x, 0, ch.z).normalized, 0.5f);

        //    var cross = Vector3.Cross(z, h);

        //    return Mathf.Clamp(cross.y * turner, -1, 1);
        //}
        //else
        //{
        //    var heading = asc.curPath.Last() - transform.position;
        //    var cross = Vector3.Cross(transform.forward, heading.normalized);
        //    return Mathf.Clamp(cross.y * turner, -1, 1);
        //}

    }


    internal Vector2 GatherInputs()
    {




        if (Vector3.Project(asc.curTargetPos() - transform.position, transform.forward).magnitude < 1)
            asc.NextTg();



        //Debug.DrawLine(transform.position, asc.curPath.Last() + Vector3.up, Color.blue);

        var correctedVeclocity = GetComponent<Rigidbody>().velocity.normalized;

        Debug.DrawRay(transform.position + Vector3.up * 2, transform.forward * 10, Color.blue);
        Debug.DrawRay(transform.position + Vector3.up * 4, correctedVeclocity * 10, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * 4, Vector3.Lerp(new Vector3(correctedVeclocity.x, 0, correctedVeclocity.z).normalized,
                                                              new Vector3(transform.parent.GetChild(0).transform.forward.x, 0, transform.parent.GetChild(0).transform.forward.z).normalized, 0.5f) * 10, Color.black);


        var angle = AngleToTurn();

        var speed = Mathf.Max(0.1f, 1 - Mathf.Abs(angle));
        if (speed < 0.1f && GetComponent<Rigidbody>().velocity.magnitude > 2)
        {
            speed = -1f;
            angle = Mathf.Sign(angle);
        }

        //Debug.Log("Speed: " + speed.ToString() + "---Angle: " + angle.ToString());


        //var angle = Input.GetAxis("Horizontal");
        //var speed = Input.GetAxis("Vertical");


        //Debug.Log(Vector3.Project(transform.forward, asc.curTargetPos() - transform.position).magnitude);
        //Debug.Log(Vector3.Project(asc.curTargetPos() - transform.position, transform.forward).magnitude);
        //Debug.Log("----");



        //Debug.Log(speed);
        //Debug.Log(angle);


        DrawBox(asc.curTargetPos(), Vector3.one + Vector3.up * 3, Quaternion.identity, Color.green);


        return new Vector2(angle, speed * slow);
        return BasicAI();
    }


}