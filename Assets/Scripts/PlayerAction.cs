﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : CharacterAction {

    public void Use () {
        if (this.hold == null) {
            return;
        }
        this.hold.Use ();
    }

    public void Update () {

    }

    public void Drop () {
        if (hold == null) {
            return;
        }

        hold.gameObject.transform.parent = null;
        hold.GetComponent<Collider2D> ().enabled = true;
        hold.isHeld = false;
        hold = null;
        touch = null;
    }

	public void Throw()
	{
        Item item = hold;
        StartCoroutine(TemporaryIgnore(item));
        Drop();
        item.velocity += (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) + 1) * Vector2.right * transform.localScale.x;
        item.velocity -= Vector2.up * 8;
	}
}