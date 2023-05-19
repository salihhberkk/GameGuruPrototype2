using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseGateValue : MonoBehaviour
{
    public AttributeSC attributeSC;

    public float GetValue()
    {
        return attributeSC.Value;
    }
}
