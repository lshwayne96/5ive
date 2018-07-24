using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    public static Texture2D texture2D;

    public static bool hasOverwritten;

    public static void OverwriteSuccessful() {

    }


    private void OnGUI() {
        if (hasOverwritten) {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.fontSize = 40;
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(10, 10, 100, 100), "Overwrite Successful", gUIStyle);
        }
    }
}
