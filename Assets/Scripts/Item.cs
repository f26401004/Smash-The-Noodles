using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public bool isHeld = false;
	private float velocity = 0;
	public float acceleration = 10;

	public const int Gap = 5;
	private int gapCounter = 0;

    public string key;
	public enum ItemType
	{
		Pizza, Suasage, Noodles, Glue, Paint, Weapon
	}

	public ItemType type;

	public void Use()
	{

	}

	public void Update()
	{
		if (!isHeld)
		{
			if (gapCounter >= Gap)
			{
				gapCounter = 0;
				var hits = Physics2D.RaycastAll(transform.position + Vector3.down * 0.4f, Vector2.down, velocity * Time.deltaTime);
				bool wasHit = false;
				foreach (var hit in hits)
				{
					if (hit.collider.gameObject == gameObject)
					{
						continue;
					}

					if (hit.collider != null)
					{
						wasHit = true;
						break;
					}
				}

				if (!wasHit)
				{
					velocity += acceleration * Time.deltaTime * Gap;
				}
				else
				{
					velocity = 0;
				}

			}

			gapCounter++;

			transform.Translate(0, -velocity * Time.deltaTime, 0);
		}
	}
		
}
