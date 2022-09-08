using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("changePrefab", 5.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changePrefab()
    {
        Debug.Log("begin");
        growth[] children =  this.GetComponentsInChildren<growth>();
        foreach (var child in children)
        {
            child.changePrefab();
        }

    }
}
