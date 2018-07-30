using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    private static Image image;
    private static Text text;
    private Coroutine currentCoroutine;

    private static float startTime;
    private float visibleDuration;

    private bool hasVisibleMessage;
    private static bool hasNewMessage;
    private bool doesFileAlreadyExist;

    private void Start() {
        image = GetComponentInChildren<Image>();
        image.enabled = false;
        text = GetComponentInChildren<Text>();

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
        text.text = notification.GetMessage();
        hasNewMessage = true;
    }

    private IEnumerator Disappear(float start) {
        hasVisibleMessage = true;

        float difference = Time.time - start;
        while (difference < visibleDuration) {
            yield return null;
        }

        CleanUp();
    }

    private static void SetUp() {
        image.enabled = true;
        startTime = Time.time;
    }

    private void CleanUp() {
        image.enabled = false;
        text.text = System.String.Empty;
        hasVisibleMessage = false;
        currentCoroutine = null;
    }
}
