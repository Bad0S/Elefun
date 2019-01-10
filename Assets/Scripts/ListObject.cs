using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Cube
{
    public float posX;
    public float posY;
    
}

[CreateAssetMenu]
public class ListObject : ScriptableObject
{
    public List<Cube> cubes;
}
