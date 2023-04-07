using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{

    public static Loading loading;
    public static Dictionary<string, List<Dictionary<string, object>>> inScene;
    public static Dictionary<string, string> prefab3D;


    public enum LoadingType
    {
        None, LoadLevel1,LoadMenu
    }
}
