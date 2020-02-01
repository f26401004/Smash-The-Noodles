using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repair : MonoBehaviour
{
	public SpriteRenderer repair;
	public Sprite repaired;
	public Image HP_bar;
	public GameObject repairEffect;

	public int hp = 0;

	public void Update()
	{
		if (hp < 100)
		{
			foreach (var item in FindObjectsOfType<Item>())
			{
				if (!item.isHeld && hp < 100 && item.type != Item.ItemType.Weapon && (item.transform.position - transform.position).sqrMagnitude < 2)
				{
					Destroy(item.gameObject);
					AddToHp(item.repairPower);
					break;
				}
			}
		}
	}

	public void AddToHp(int value)
	{
		var effect = Instantiate(repairEffect);
		effect.transform.position = transform.position;
		Destroy(effect, 2);

		hp = Mathf.Clamp(hp + value, 0, 100);

		// TODO: Update UI
		if(HP_bar){
			HP_bar.fillAmount = (float)hp / 100.0f;
			if(hp > 70){
				HP_bar.color = new Color(0, 0.8f, 0);
			}
		}

		if (hp == 100)
		{
			repair.sprite = repaired;
		}
	}
}
