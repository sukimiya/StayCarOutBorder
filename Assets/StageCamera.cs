using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCamera : MonoBehaviour {

    public LineRenderer target;

    void Start()
    {
        
    }

    void LateUpdate()
    {//执行完所有update之后再执行的程序，为了防止出现摄像机的update先执行而出现的闪烁，所有相机的函数都用lateupdate
        
    }

    // Update is called once per frame
    void Update () {
        target = GameObject.Find("lineTemplete").GetComponent<LineRenderer>();
        if (target)
        {
            Vector3 v1 = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = v1;
        }
    }
}
