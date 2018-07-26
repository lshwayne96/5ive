/*
 * This script encapsulates the functionality of a lever.
 * It requires the gameObject of interest to be dragged to interactable.
 */

using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable {

    // Expose the interactable variable to the editor
    public GameObject interactable;

    private bool hasRotated;
    private bool interactableState;
    private bool hasEnteredTrigger;
    private Vector3 currentAngle;
    private Vector3 targetAngle;
    private float angleOfRotation;

    private float startTime;
    private float journeyLength;
    private float speed;

    private Quaternion startRotation;
    private Quaternion endRotation;

    /*
     * Awake() is used instead of Start() to allow the lever
     * to be rotated by Restore() in InteractablesData
     * to its previous state when a new game is loaded
     * */
    void Awake() {
        interactableState = interactable.activeSelf;
        angleOfRotation = 90f;

        Vector3 angleDifference = Vector3.back * angleOfRotation;
        currentAngle = transform.eulerAngles;
        targetAngle = transform.eulerAngles + angleDifference;

        startRotation = transform.rotation;
        Vector3 eulerAngles = transform.eulerAngles;
        endRotation = Quaternion.Euler(eulerAngles + angleDifference);
    }

    void Start() {
        speed = 300f;
        journeyLength = Vector3.Distance(targetAngle, currentAngle);
    }

    /* 
     * If the player is within the collider boundaries of the lever
     * and the R key is pressed, the lever will rotate and
     * and the interactable gameObject will disappear.
    */
    void Update() {
        if (hasEnteredTrigger && Input.GetKeyUp(KeyCode.R)) {
            startTime = Time.time;
            StartCoroutine(Move());
        }
    }

    // When the player enters the lever
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = true;
        }
    }

    // When the player leaves the lever
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            hasEnteredTrigger = false;
        }
    }

    private IEnumerator Move() {
        float distCovered;
        float fracJourney = 0;

        while (fracJourney < 1) {
            // Distance moved = time * speed.
            distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, fracJourney);

            yield return null;
        }

        ChangeInteractableState();
        Quaternion temp = startRotation;
        startRotation = endRotation;
        endRotation = temp;
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
        interactable.SetActive(!interactableState);
        interactableState = !interactableState;
    }

    public void Toggle() {
        Move();
        ChangeInteractableState();
    }

    public bool HasToggled() {
        return hasRotated;
    }
}
