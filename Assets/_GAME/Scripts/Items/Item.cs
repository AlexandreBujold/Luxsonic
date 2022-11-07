using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Identifies an object as a specific item by its ID.
/// </summary>
public class Item : MonoBehaviour
{
    [field: SerializeField]
    public int ID { get; private set; } = 0;

    public static bool IsItemID(Item item, int ID) => item.ID == ID;
}
