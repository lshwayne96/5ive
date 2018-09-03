/*
 * This script enables the attached gameObject,
 * namely a ladder, to interact with the player.
 * The interactions include climbing up and down,
 * and simply walking through it.
 */
using UnityEngine;

public class Ladder : MonoBehaviour, ICacheable<LadderData> {

    // Expose the climbingSpeed variable to the editor
    public readonly float climbingSpeed = 6;

    // Is the player simply passing through the ladder?
    public bool IsPassingThrough { get; private set; }
    // Was the player climbing (and has now stopped on it)?
    public bool IsClimbing { get; private set; }
    public bool OutsideLadder { get; private set; }
    public bool CanClimb { get; private set; }
    public float OriginalGravityScale { get; private set; }
    private bool hasRestored;

    public TopOfLadder TopOfLadder { get; private set; }
    private Rigidbody2D playerRb;


    void Start() {
        IsPassingThrough = true;
        OutsideLadder = true;

        TopOfLadder = GetComponentInChildren<TopOfLadder>();
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();

        if (!hasRestored) {
            OriginalGravityScale = playerRb.gravityScale;
        }
    }

    // While on the ladder
    private void FixedUpdate() {
        if (CanClimb) {
            // Get the x of the velocity since only the y should change
            float current_x = playerRb.velocity.x;

            if (Input.GetKey(KeyCode.UpArrow)) {
                MoveUp(current_x);

            } else if (Input.GetKey(KeyCode.DownArrow)) {
                MoveDown(current_x);

            } else {
                Stop(current_x);
            }
        }
    }

    private void MoveUp(float currentX) {
        playerRb.velocity = new Vector2(currentX, climbingSpeed);
        IsClimbing = true;
    }

    private void MoveDown(float currentX) {
        playerRb.velocity = new Vector2(currentX, -climbingSpeed);
        IsClimbing = true;
    }

    private void Stop(float currentX) {
        if (IsPassingThrough) { // In front of ladder
            IsClimbing = false;
        } else { // On ladder
            playerRb.velocity = new Vector2(currentX, 0);
            // Remove the effects of gravity on the player
            playerRb.gravityScale = 0;
        }
    }


    // When entering the ladder
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.UpArrow)) {
                CanClimb = true;
                OutsideLadder = false;
                IsPassingThrough = false;
            }
        }
    }

    // When on the ladder
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            CanClimb = true;
            OutsideLadder = false;

            if (IsClimbing) {
                IsPassingThrough = false;
            }
        }
    }

    // When leaving the ladder
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            CanClimb = false;
            OutsideLadder = true;
            IsClimbing = false;
            IsPassingThrough = true;
            // Revert back so the player doesn't go flying
            playerRb.gravityScale = OriginalGravityScale;
        }
    }

    public LadderData CacheData() {
        return new LadderData(this);
    }

    public void Restore(LadderData ladderData) {
        IsPassingThrough = ladderData.IsPassingThrough;
        IsClimbing = ladderData.IsClimbing;
        OutsideLadder = ladderData.OutsideLadder;
        CanClimb = ladderData.CanClimb;
        GetComponent<BoxCollider2D>().isTrigger = ladderData.IsTrigger;
        OriginalGravityScale = ladderData.OriginalGravityScale;
        hasRestored = true;
    }
}
