using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Fish/Skin")]
public class Skin : ScriptableObject
{
    [HideInInspector] public static int SIZE = 4;
    public Material[] materials = new Material[SIZE];
}
