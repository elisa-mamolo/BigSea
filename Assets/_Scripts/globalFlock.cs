using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    //changed from 5 to 7
    public static int tankSize = 7;
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    //number of fish we want to create
    static int numFish = 50;
    //add the fishes in the array that contains all fishes
    public static GameObject[] allFish = new GameObject[numFish];

    //initial postion in the middle of tank
    public static Vector3 goalPos = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        //render setting construct for fog
        //background of fog and camera need to be the same to blend
        //could set to another color and will have a pink, blue fog
        
        //intanciate fishes before the app start
        for( int i = 0; i < numFish; i++)
        {
            //creating a position for our fish in 3d space
            //using random range between the value of tank size
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            //instanciate the fish prefab and stick it into the array
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // every 50 in 10000 times it will reset and randomly move around
        if(Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                 Random.Range(-tankSize, tankSize),
                                 Random.Range(-tankSize, tankSize));

            goalPrefab.transform.position = goalPos;
        }
    }
}
