using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
	public SpriteRenderer repair;
	public Sprite repaired;

	public int hp = 0;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		Item item = collision.GetComponent<Item>();
		if (!item || item.type == Item.ItemType.Weapon)
		{
			return;
		}
		AddToHp(10);
		Destroy(item.gameObject);
	}

	public void AddToHp(int value)
	{
		hp  = Mathf.Max(hp + value, 100);
		// TODO: Update UI
		if (hp == 100)
		{
			repair.sprite = repaired;
		}
	}
}
