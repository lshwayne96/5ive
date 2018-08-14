/*
 * This script manages the sending of notifications.
 */

using System.Collections;
using UnityEngine;

public class NotificationManager : MessageManager {

    public override float visibleDuration {
        get { return 3f; }
    }

    // Makes the notification disappear after a certain duration
    protected override IEnumerator Disappear() {
        hasVisibleMessage = true;
        float difference = 0;

        while (difference < visibleDuration) {
            difference = Time.time - startTime;
            yield return null;
        }

        CleanUp();
    }
}
