using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour
{
	public SpriteRenderer repair;
	public Sprite repaired;

	public int hp = 0;

	public void Update()
	{
		if (hp < 100)
		{
			foreach (var item in FindObjectsOfType<Item>())
			{
				if (!item.isHeld && item.type != Item.ItemType.Weapon && (item.transform.position - transform.position).sqrMagnitude < 2)
				{
					Destroy(item.gameObject);
					AddToHp(10);
					break;
				}
			}
		}
	}

	public void AddToHp(int value)
	{
		hp  = Mathf.Min(hp + value, 100);
		// TODO: Update UI
		if (hp == 100)
		{
			repair.sprite = repaired;
		}
	}
}
