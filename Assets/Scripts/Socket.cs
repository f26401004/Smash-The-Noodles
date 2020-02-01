using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitJson;
using Nakama;
using UnityEngine;
using UnityEngine.UI;

public class Socket : MonoBehaviour {
    // Set of all items.
    public Dictionary<string, Item> itemSet = new Dictionary<string, Item> ();

    // Item Prefabs
    public Item Pizza;

	private Item GenerateItem(string type, Vector3 position, string key)
	{
        // TODO: 生成物品
        Item item = null;
		if (type == "Pizza")
		{
            item = Instantiate(Pizza);
            item.type = Item.ItemType.Pizza;
		}
        item.transform.position = position;
        item.key = key;
        return item;
	}
}