using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class growth : MonoBehaviour
{
    private string dir ;
    private string objectName;
    private int currentVisu = 0;
    // Start is called before the first frame update
    void Start()
    {
        objectName = "Oak";
        dir = "Prefabs/" +objectName +"/";
        InvokeRepeating("changePrefab", 10.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void changePrefab()
    {
        object[] allVisu = Resources.LoadAll(dir);

        GameObject current = this.transform.GetChild(0).gameObject;
        currentVisu= (currentVisu+1)%allVisu.Length;
        GameObject newVisu = Resources.Load(dir + objectName+"_"+ currentVisu.ToString() )as GameObject;
        newVisu = Instantiate(newVisu);
        newVisu.transform.position = current.transform.position;
        newVisu.transform.rotation = current.transform.rotation;
        newVisu.transform.parent = current.transform.parent;
        Destroy(current);
    }
}
