using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Transform scoreBar;
	public Text scoreText;
	public Repair[] repairs;
	public float scoreBarExtendTo;

	public void Update()
	{
		float score = 0;
		foreach (var repair in repairs)
		{
			score += repair.hp;
		}
		score /= (100 * repairs.Length);

		scoreText.text = Mathf.Floor(score * 100).ToString() + "%";
		scoreBar.localScale = new Vector3(score * scoreBarExtendTo, scoreBar.localScale.y, 1);
	}
}
