using System.Collections;
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
}