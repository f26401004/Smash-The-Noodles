﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public bool isHeld = false;
	public Vector2 velocity = Vector2.zero;
	public float acceleration = 10;
	public int repairPower;

	public const int Gap = 5;
	private int gapCounter = 0;
	private SpriteRenderer sprite;
	public Gradient gradient;
	public float duration;
	private float animationCounter = 0;

    public string key;
	public enum ItemType
	{
		Pizza, Suasage, Noodles, Glue, Paint, Weapon
	}

	public ItemType type;

	public void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(1, 1, 1, 0);
	}

	public void Update()
	{
		if (animationCounter < duration)
		{
			sprite.color = gradient.Evaluate(animationCounter / duration);
			animationCounter += Time.deltaTime;
		}

		if (!isHeld)
		{
			if (gapCounter >= Gap)
			{
				gapCounter = 0;
				var hits = Physics2D.RaycastAll(transform.position + (velocity.y >= 0 ? Vector3.down : Vector3.up) * 0.4f, (velocity.y >= 0 ? Vector3.down : Vector3.up), Mathf.Abs(velocity.y) * Time.deltaTime * 3);
				bool wasHit = false;
				foreach (var hit in hits)
				{
					if (hit.collider.gameObject == gameObject || hit.collider.gameObject.layer == 8 || hit.collider.GetComponent<Item>())
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
					velocity = new Vector2(velocity.x * 0.99f, 0);
				}

				hits = Physics2D.RaycastAll(transform.position + (velocity.x < 0 ? Vector3.left : Vector3.right) * 0.4f, velocity.x < 0 ? Vector2.left : Vector2.right, Mathf.Abs(velocity.x) * Time.deltaTime * 3);
				wasHit = false;
				foreach (var hit in hits)
				{
					if (hit.collider.gameObject == gameObject || hit.collider.gameObject.layer == 8 || hit.collider.GetComponent<Item>())
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
			velocity = new Vector2(velocity.x * 0.99f, velocity.y);
			transform.Translate(velocity.x * Time.deltaTime, -velocity.y * Time.deltaTime, 0, Space.World);
		}
		else
		{
			transform.localPosition = Vector3.zero;
			velocity = Vector2.zero;
		}

		if (isHeld && type == ItemType.Weapon)
		{
			transform.localEulerAngles = new Vector3(0, 0, 30);
		}

		if (type == ItemType.Weapon && velocity.sqrMagnitude > 0.05f)
		{
			float multiplier = velocity.x > 0 ? -1 : 1;
			transform.Rotate(0, 0, velocity.sqrMagnitude * 30 * Time.deltaTime * multiplier);
		}
	}
		
}
