using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform pathHolder;
    [SerializeField] private float speed = 5;
    [SerializeField] private float waitTime = 0.3f;
    [SerializeField] private float turnSpeed = 90.0f;

    [SerializeField] private Light spotlight;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask viewMask;

    private float _viewAngle;
    private Transform _player;
    private Color _originalSpotlightColor;

    private void OnDrawGizmos()
    {
        DrawPath();
        DrawViewDistance();
    }


    private void DrawViewDistance()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    
    private void DrawPath()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            // Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(waypoint.position, 0.2f);
            // Gizmos.DrawIcon(waypoint.position, "AvatarSelector", true);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }

        Gizmos.DrawLine(previousPosition, startPosition);
    }


    private Vector3[] GetWaypointPosition()
    {
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x,
                transform.position.y,
                waypoints[i].z);
        }

        return waypoints;
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetWaypoint,
                speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }

            yield return null;
        }
    }


    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y,
                targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, _player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (_player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < _viewAngle / 2.0f)
            {
                if (!Physics.Linecast(transform.position, _player.position, viewMask))
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _viewAngle = spotlight.spotAngle;
        _originalSpotlightColor = spotlight.color;
        StartCoroutine(FollowPath(GetWaypointPosition()));
    }


    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            spotlight.color = Color.red;
        }
        else
        {
            spotlight.color = _originalSpotlightColor;
        }
    }
}