using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string key;
	public enum ItemType
	{
		Pizza, Suasage, Noodles, Glue, Paint, Weapon
	}

	public ItemType type;

	public void Use()
	{

	}
}
