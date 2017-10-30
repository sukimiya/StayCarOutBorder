using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PathLoader: MonoBehaviour
{

    public static Lineation LoadJsonFromFile()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (!File.Exists(Application.dataPath + "/Resources/groundmapsettings.json"))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/groundmapsettings.json");

        //FileStream file = File.Open(Application.dataPath + "/Test.json", FileMode.Open, FileAccess.ReadWrite);
        //if (file.Length == 0)
        //{
        //    return null;
        //}

        //string json = (string)bf.Deserialize(file);
        //file.Close();

        if (sr == null)
        {
            return null;
        }
        string json = sr.ReadToEnd();
        Debug.Log(json);
        if (json.Length > 0)
        {
            return JsonUtility.FromJson<Lineation>(json);
        }

        return null;
    }
}
