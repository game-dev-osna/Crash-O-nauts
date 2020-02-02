using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePath : MonoBehaviour
{
    VertexPath path;
    float t;
    public Transform obj;
    float speed;

    void Start()
    {
        //Init(new Vector3(0, 0, 0), new Vector3(10, 0, 0), obj, 0.4f);
    }

    Vector3 generateRandomVector()
    {
        return new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    public void Init(Vector3 start, Vector3 end, Vector3 startNormal, Vector3 endNormal, Transform obj, float speed)
    {
        Vector3[] waypoints = new Vector3[4];

        waypoints[0] = start;
        waypoints[1] = start + startNormal * 15.0f;
        waypoints[2] = end + endNormal * 15.0f;
        waypoints[3] = end;

        // Create a new bezier path from the waypoints.
        BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);
        PathCreator creator = GetComponent<PathCreator>();
        creator.bezierPath = bezierPath;
        Debug.Log(creator.bezierPath.AutoControlLength);
        path = creator.path;

        t = 0.0f;

        this.speed = speed;
        this.obj = obj;
    }

    public void Init(Vector3 start, Vector3 end, Transform obj, float speed)
    {
        Vector3[] waypoints = new Vector3[4];

        Vector3 direction = end - start;
        float distance = direction.magnitude;

        waypoints[0] = start;
        waypoints[1] = start + direction * 0.2f + generateRandomVector() * distance * 0.1f;
        waypoints[2] = start + direction * 0.8f + generateRandomVector() * distance * 0.1f;
        waypoints[3] = end;

        // Create a new bezier path from the waypoints.
        BezierPath bezierPath = new BezierPath(waypoints, false, PathSpace.xyz);
        PathCreator creator = GetComponent<PathCreator>();
        creator.bezierPath = bezierPath;
        path = creator.path;

        t = 0.0f;

        this.speed = speed;
        this.obj = obj;
    }

    public bool AnimatePath()
    {
        //if (!obj)
            //return;

        t += Time.deltaTime * speed;
        obj.position = path.GetPointAtDistance(t, EndOfPathInstruction.Stop);
        obj.rotation = path.GetRotationAtDistance(t, EndOfPathInstruction.Stop);

        if (path.length > t)
            return false;
        return true;
    }
}
