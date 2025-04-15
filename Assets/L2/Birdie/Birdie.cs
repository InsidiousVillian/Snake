using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum BirdStates
{
    Moving,
    Diving,
    Retreat
}

public class Birdie : MonoBehaviour
{
    [SerializeField]
    float turningSpeed = 50.0f;
    [SerializeField]
    float birdSpeed = 10.0f;





    [SerializeField]
    GameObject[] waypoints;

    [SerializeField]
    Vector3 currentTarget;

    [SerializeField]
    float Closeness;
    
    

    BirdStates currentState = BirdStates.Moving;
    //vision

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTarget = GameObject.Find("SnakeH").gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Vector: " + currentTarget.ToString());   
        //
        switch (currentState)
        {
            case BirdStates.Moving:
                //do this code for movement

                if (currentTarget == Vector3.zero)
                    currentTarget = waypoints[Random.Range(0, waypoints.Length)].transform.position;

                //get current rotation
                Quaternion startRotation = transform.rotation;
                //look for target
                transform.LookAt(currentTarget);
                
                //store target 
                Quaternion targetRotation = transform.rotation;
                transform.rotation = startRotation;

                //rotation towards
                transform.rotation = Quaternion.RotateTowards(startRotation, targetRotation, turningSpeed);

                //movement
                transform.position = transform.position + transform.forward * Time.deltaTime * birdSpeed;


                //if the bird has moved close enough to a waypoint
                float distance = Vector3.Distance(transform.position, currentTarget);

                if (distance < Closeness)
                {
                    //waypoint
                    currentTarget = waypoints[Random.Range(0, waypoints.Length)].transform.position;
                }




                break;
            case BirdStates.Diving:
                //do this code
                break;
            case BirdStates.Retreat:
                //do retreat
                break;
            default:
                break;
        }
    }
}
