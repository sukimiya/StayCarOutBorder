using UnityEngine;
using System.Collections;

public class MainMonoBehavior : MonoBehaviour
{

    public delegate void MainEventHandler(GameObject dispatcher);
    public event MainEventHandler StartEvent;
    public event MainEventHandler UpdateEvent;
    public void Start()
    {
        ResourceManager.getInstance().LoadSence("resources/groundmapsettings.txt");//json配置文件
        if (StartEvent != null)
        {
            StartEvent(this.gameObject);
        }
    }
    public void Update()
    {
        if (UpdateEvent != null)
        {
            UpdateEvent(this.gameObject);
        }
    }
}