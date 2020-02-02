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
		score = Mathf.Floor(score * 100);

		scoreText.text = score.ToString() + "%";
		scoreBar.localScale = new Vector3(score / (100 * repairs.Length) * scoreBarExtendTo, scoreBar.localScale.y, 1);
	}
}
