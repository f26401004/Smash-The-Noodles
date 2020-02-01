using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public bool isHeld = false;
	public Vector2 velocity = Vector2.zero;
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
				var hits = Physics2D.RaycastAll(transform.position + (velocity.y >= 0 ? Vector3.down : Vector3.up) * 0.4f, (velocity.y >= 0 ? Vector3.down : Vector3.up), Mathf.Abs(velocity.y) * Time.deltaTime);
				bool wasHit = false;
				foreach (var hit in hits)
				{
					if (hit.collider.gameObject == gameObject || hit.collider.GetComponent<PlayerAction>())
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
					velocity += acceleration * Time.deltaTime * Gap * Vector2.up;
				}
				else
				{
					velocity = new Vector2(velocity.x, 0);
				}

				hits = Physics2D.RaycastAll(transform.position + (velocity.x < 0 ? Vector3.left : Vector3.right) * 0.4f, velocity.x < 0 ? Vector2.left : Vector2.right, Mathf.Abs(velocity.x) * Time.deltaTime);
				wasHit = false;
				foreach (var hit in hits)
				{
					if (hit.collider.gameObject == gameObject || hit.collider.GetComponent<PlayerAction>())
					{
						continue;
					}

					if (hit.collider != null)
					{
						wasHit = true;
						break;
					}
				}

				if (wasHit)
				{
					velocity = new Vector2(0, velocity.y);
				}

			}

			gapCounter++;

			transform.Translate(velocity.x * Time.deltaTime, -velocity.y * Time.deltaTime, 0);
		}
		else
		{
			transform.localPosition = Vector3.zero;
		}
	}
		
}
