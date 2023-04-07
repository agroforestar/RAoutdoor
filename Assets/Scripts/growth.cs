using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class growth : MonoBehaviour
{
    private string dir ;
    private string objectName;
    private int currentVisu = 0;

    public int CurrentVisu { get => currentVisu; set => currentVisu = value; }

    // Start is called before the first frame update
    void Start()
    {
        objectName = "Oak";
        dir = "Prefabs/" +objectName +"/";
   //     InvokeRepeating("changePrefab", 5.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changePrefab()
    {
        object[] allVisu = Resources.LoadAll(dir);

        GameObject current = this.transform.gameObject;
        CurrentVisu= (CurrentVisu+1)%allVisu.Length;
        GameObject newVisu = Resources.Load(dir + objectName+"_"+ CurrentVisu.ToString() )as GameObject;;
        newVisu = Instantiate(newVisu);
        newVisu.transform.position = current.transform.position;
        newVisu.transform.rotation = current.transform.rotation;
        newVisu.transform.parent = current.transform.parent;
        growth componentNew = newVisu.GetComponent<growth>();
        componentNew.CurrentVisu = CurrentVisu;
        Destroy(current);
    }

    
}
