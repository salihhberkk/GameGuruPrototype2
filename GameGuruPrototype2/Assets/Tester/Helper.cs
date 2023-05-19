using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    private static Vector3 tempVector3;

    public static Vector3 Help(float x, float y , float z)
    {
        tempVector3.x = x;
        tempVector3.y = y;
        tempVector3.z = z;

        return tempVector3;
    }
}
