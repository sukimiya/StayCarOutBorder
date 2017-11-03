using UnityEngine;
using System.Collections;

public class TankCamera : MonoBehaviour
{

    public LineRenderer target;

    void Start()
    {
        target = GameObject.Find("lineTemplete").GetComponent<LineRenderer>();
    }

    void LateUpdate()
    {//执行完所有update之后再执行的程序，为了防止出现摄像机的update先执行而出现的闪烁，所有相机的函数都用lateupdate
        transform.position = target.transform.position;//摄像机的位置等于target（坦克）的位置.
    }
}