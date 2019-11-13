using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//makes the fishes swimming
public class flock : MonoBehaviour
{
    public float speed = 0.5f;
    //how fast fish will turn 
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    // max distance from each other and
    // will flock just if they are max at 2.0f
    float neighbourDistance = 2.0f;
    
    //make the fishes turn back to center when 
    //getting to the edge of the tank
    bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f,1);
    }

    // Update is called once per frame
    void Update()
    {
        //if the fishe is close to tank borders
        if(Vector3.Distance(transform.position, Vector3.zero) >= globalFlock.tankSize)
        {
            //it turns
            turning = true;
        }
        else
            //go to line 49
            turning = false;
        
        if(turning)
        {
            //calculate direction towards center of tank 
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        { 
        //run the apply rules once every 5 times randomly
        if(Random.Range(0,5) < 1)
            ApplyRules();
        }
        //fish is going to swim forward  
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlock.allFish;
        //calculate the center of the group 
        Vector3 vcentre = Vector3.zero;
        //and avoid hitting each other
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = globalFlock.goalPos;

        //distance variable
        float dist;
        //calculate the group size based on distance from neighbour 
        //see linne 54
        int groupSize = 0;
        foreach(GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                //if distance is 2 the fish is part of a group
                if (dist <= neighbourDistance)
                {
                    //add center and group size
                    vcentre += go.transform.position;
                    groupSize++;
                    //if less than 1 oway from the group
                    if(dist < 1.0f)
                    {
                        //we will go in another direction
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    //calculate speed from flock script attached to neighbour fish and have a average speed for the group at line 74
                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        //if we are in agroup
        if(groupSize > 0)
        {
            //calc avg center and speed of group
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            //if direction is not = to 0 we will change direction
            if (direction != Vector3.zero)
                //slowly turn from one rotation to another
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);

        }
    }
}
