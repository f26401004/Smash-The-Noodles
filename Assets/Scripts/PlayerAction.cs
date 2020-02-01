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
    public void TryPick (Item item) {
        // send try pick message
        socket.sendMessage (3, payload (item));
    }

    // TODO: item set
    public void Drop () {
        if (hold == null) {
            return;
        }
        // send drop message to room manager
        socket.sendMessage (6, payload (hold));

        hold.gameObject.transform.parent = null;
        hold.gameObject.transform.localPosition = Vector3.zero;
    }
}