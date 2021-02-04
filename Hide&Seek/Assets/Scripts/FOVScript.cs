using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CustomEditor (typeof (FOV))]
//public class FOVScript : Editor
//{
//    void OnSceneGUI()
//    {
//        FOV fov = (FOV)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.viewRadius);
//        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle/2,false);
//        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

//        //Handles.color = Color.white;
//        //foreach(Transform visibleTarget in fov.VisibleTargets)
//        //{
//        //    Handles.DrawLine(fov.transform.position, visibleTarget.position);
//        //}

//    }

//    //// Start is called before the first frame update
//    //void Start()
//    //{
        
//    //}

//    //// Update is called once per frame
//    //void Update()
//    //{
        
//    //}
//}


