using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ChangePostProcess : MonoBehaviour
{
    public PostProcessingProfile normal, fx;
    //create pp component and behaviour component handled by cmaera
    private PostProcessingBehaviour camImageFx;
    // Start is called before the first frame update
    void Start()
    {
        camImageFx = FindObjectOfType<PostProcessingBehaviour>(); 
    }
    //method gets triggered when another object triggers the collider
     void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            camImageFx.profile = fx;
        }
    }

    //same script on player exit
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            camImageFx.profile = normal;
        }
    }
}
