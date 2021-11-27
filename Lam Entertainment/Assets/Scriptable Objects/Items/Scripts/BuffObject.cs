using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potion")]
public class BuffObject : ItemObject
{
    public int RestoreHealthValue;
    private void Awake()
    {
        type = ItemType.Buff;

    }
}
