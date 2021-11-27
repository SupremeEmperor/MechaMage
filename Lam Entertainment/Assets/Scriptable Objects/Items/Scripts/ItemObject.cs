using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Buff,Equipment, Default}

public abstract class ItemObject : ScriptableObject
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public ItemType type;
    [SerializeField]
    [TextArea(15, 20)]
    public string description;

}
