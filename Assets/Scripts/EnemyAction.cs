using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : CharacterAction {
    public void Pick (Item item) {
        if (hold) {
            socket.sendMessage (6, payload (hold));
            hold.parent = null;
        }
        item.gameObject.transform.parent = transform;
        item.gameObject.transform.localPosition = Vector3.zero;
        hold = item;
    }
}