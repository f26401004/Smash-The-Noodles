using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{

	public const float DeathLine = -8;
    void Update()
    {
        foreach (var item in FindObjectsOfType<Item>())
		{
			if (!item.isHeld && item.transform.position.y < DeathLine)
			{
				Destroy(item.gameObject);
			}
		}

		foreach (var player in FindObjectsOfType<PlayerAction>())
		{
			if (player.gameObject.activeInHierarchy && player.transform.position.y < DeathLine)
			{
				FindObjectOfType<Control>().LetDie(player);
			}
		}
    }

	private void OnDrawGizmos()
	{
		Gizmos.DrawLine(new Vector3(-100, -8, 0), new Vector3(100, -8, 0));
	}
}
