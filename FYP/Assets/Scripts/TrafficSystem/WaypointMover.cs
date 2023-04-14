using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 5f;
    public float safeDistance = 5f;

    private Transform currentWaypoint;
    private bool isStopMove;

    // Start is called before the first frame update
    void Start()
    {
        //set initial position of the first waypoint
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //set the next waypoint target
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);

        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStopMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
                transform.LookAt(currentWaypoint);
            }

        }

        Vector3 carOfset = new Vector3(0,0.5f, 0);
        Vector3 direction = Vector3.forward;

        Ray theRay = new Ray(transform.position + carOfset, transform.TransformDirection(direction * safeDistance) );
        Debug.DrawRay(transform.position + carOfset, transform.TransformDirection(direction * safeDistance) , Color.blue);

        if (Physics.Raycast(theRay, out RaycastHit hit, 5f))
        {
            if (hit.collider.tag == "AICar" || hit.collider.tag == "Player" )
            {
                isStopMove = true;
                transform.position += new Vector3(0, 0, 0);
            }
        }
        else
        {
            isStopMove = false;
        }

        Vector3 carOfset2 = new Vector3(0.5f, 0.5f, 0);
        Vector3 direction2 = Vector3.forward;
        Ray theRay2 = new Ray(transform.position + carOfset2, transform.TransformDirection(direction2 * safeDistance + carOfset2));
        Debug.DrawRay(transform.position + carOfset2, transform.TransformDirection(direction2 * safeDistance + carOfset2) , Color.blue);

        if (Physics.Raycast(theRay2, out RaycastHit hit2, safeDistance))
        {
            if (hit2.collider.tag == "AICar" || hit2.collider.tag == "Player")
            {
                //Debug.Log("hhbit");
                isStopMove = true;
                transform.position += new Vector3(0, 0, 0);
            }
        }
        else
        {
            //Debug.Log("Notin it");
            isStopMove = false;
        }

        Vector3 carOfset3 = new Vector3(-0.5f, 0.5f, 0);
        Vector3 direction3 = Vector3.forward;
        Ray theRay3 = new Ray(transform.position + carOfset3, transform.TransformDirection(direction3 * safeDistance + carOfset3));
        Debug.DrawRay(transform.position + carOfset3, transform.TransformDirection(direction3 * safeDistance + carOfset3), Color.blue);

        if (Physics.Raycast(theRay3, out RaycastHit hit3, safeDistance))
        {
            if (hit3.collider.tag == "AICar" || hit3.collider.tag == "Player")
            {
                isStopMove = true;
                transform.position += new Vector3(0, 0, 0);
            }
        }
        else
        {
            isStopMove = false;
        }
    }
}
