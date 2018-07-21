/*
 * This script encapsulates the functionality of a lever.
 * It requires the gameObject of interest to be dragged to interactable.
 */

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

    /*
     * Awake() is used instead of Start() to allow the lever
     * to be rotated by Restore() in InteractablesData
     * to its previous state when a new game is loaded
     * */
    void Awake() {
        interactableState = interactable.activeSelf;
        angleOfRotation = 90f;

        // Cache both the currentAngle and targetAngle for convenience
        currentAngle = transform.eulerAngles;
        targetAngle = transform.eulerAngles + Vector3.back * angleOfRotation;
    }

    /* 
     * If the player is within the collider boundaries of the lever
     * and the R key is pressed, the lever will rotate and
     * and the interactable gameObject will disappear.
    */
    void Update() {
        if (hasEnteredTrigger && Input.GetKeyUp(KeyCode.R)) {
            RotateLever();
            ChangeInteractableState();
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

    private void RotateLever() {
        transform.eulerAngles = Vector3.Lerp(currentAngle, targetAngle, 1);
        hasRotated = !hasRotated;

        // Swap the currentAngle and targetAngle
        Vector3 temp;
        temp = currentAngle;
        currentAngle = targetAngle;
        targetAngle = temp;
    }

    // Controls the interactable assigned to the lever
    private void ChangeInteractableState() {
        interactable.SetActive(!interactableState);
        interactableState = !interactableState;
    }

    public void Toggle() {
        RotateLever();
        ChangeInteractableState();
    }

    public bool HasToggled() {
        return hasRotated;
    }
}
