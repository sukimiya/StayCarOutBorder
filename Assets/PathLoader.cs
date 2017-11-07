using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PathLoader: MonoBehaviour
{

    public static GPath LoadJsonFromFile()
    {
        TextAsset textjson = Resources.Load<TextAsset>("groundmapsettings");
        return JsonUtility.FromJson<GPath>(textjson.text);
    }
    public static carData loadCarData()
    {

        TextAsset textjson = Resources.Load<TextAsset>("car");
        return JsonUtility.FromJson<carData>(textjson.text);
        
    }
    public static RunPath loadPath()
    {
        TextAsset textjson = Resources.Load<TextAsset>("allpathdata");
        return JsonUtility.FromJson<RunPath>(textjson.text);
        
    }
}
