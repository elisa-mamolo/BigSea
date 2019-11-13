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


    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //run the apply rules once every 5 times randomly
        if (Random.Range(0.5) < 1)
            ApplyRules();
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
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if(dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
        }
    }
}
