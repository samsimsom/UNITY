using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Coroutines allow one to spread the execution of code
 * over multiple frames using the yield statement.
 *
 * pause coroutine for two seconds.
 * yield return new WaitForSeconds(2.0f);
 *
 * pause coroutine until next frame.
 * yield return null;
 *
 * pause coroutine until "DoSomething()" has finished running.
 * yield return StartCoroutine(DoSomething());
 *
 * To stop a coroutine,
 * you'll need a reference to it to pass into the StopCoroutine() method:
 * IEnumerator currentCoroutine = DoSomething();
 * StartCoroutine(currentCoroutine);
 * StopCoroutine(currentCoroutine);
 *
 * You can only start and stop coroutines from a class that inherits from MonoBehaviour.
 */

public class Test01 : MonoBehaviour
{
    [SerializeField] private Transform[] path;
    private IEnumerator _currentMoveCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        string[] messages = { "Welcome", "to", "this", "amazing", "game!" };
        StartCoroutine(PrintMessages(messages, 1.0f));
        StartCoroutine(FollowPath());
    }


    // Update is called once per frame
    void Update()
    {
        // Player Input KeyDown-Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_currentMoveCoroutine != null)
            {
                StopCoroutine(_currentMoveCoroutine);
            }

            _currentMoveCoroutine = Move(Random.onUnitSphere * 5, 8);
            StartCoroutine(_currentMoveCoroutine);
        }
    }


    // FollowPath Coroutine
    IEnumerator FollowPath()
    {
        foreach (Transform waypoint in path)
        {
            yield return StartCoroutine(Move(waypoint.position, 8));
        }
    }


    // Move destination coroutine
    IEnumerator Move(Vector3 destination, float speed)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                destination,
                speed * Time.deltaTime);
            yield return null;
        }
    }


    // Text Messages Coroutine
    IEnumerator PrintMessages(string[] message, float delay)
    {
        foreach (string msg in message)
        {
            Debug.Log(msg);
            yield return new WaitForSeconds(delay);
        }
    }
}