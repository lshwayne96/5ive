/*
 * This script manages the sending of notifications.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    private static Image notificationBox;
    private static Text message;
    private Coroutine currentCoroutine;

    private static float startTime;
    private float visibleDuration;

    private bool hasVisibleMessage;
    private static bool hasNewMessage;
    private bool doesFileAlreadyExist;

    private void Start() {
        notificationBox = GetComponentInChildren<Image>();
        // Make the notification box invisible
        notificationBox.enabled = false;
        message = GetComponentInChildren<Text>();

        visibleDuration = 3f;
    }

    private void Update() {
        if (hasNewMessage) {
            if (hasVisibleMessage) {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(Disappear(startTime));
        }
    }

    public static void Notifiy(Notification notification) {
        SetUp();
        message.text = notification.GetMessage();
        hasNewMessage = true;
    }

    // Makes the notification disappear after a certain duration
    private IEnumerator Disappear(float start) {
        hasVisibleMessage = true;

        float difference = Time.time - start;
        while (difference < visibleDuration) {
            yield return null;
        }

        CleanUp();
    }

    private static void SetUp() {
        // Make the notification box visible
        notificationBox.enabled = true;
        startTime = Time.time;
    }

    private void CleanUp() {
        // Make the notification box invisible
        notificationBox.enabled = false;
        // Clear the notification box of any messages
        message.text = System.String.Empty;
        hasVisibleMessage = false;
        hasNewMessage = false;
        currentCoroutine = null;
    }
}
