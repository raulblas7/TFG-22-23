using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Flow/Skin")]
public class Skin : ScriptableObject
{
    [HideInInspector] public static int SIZE = 16;
    public Color[] colors = new Color[SIZE];
}
