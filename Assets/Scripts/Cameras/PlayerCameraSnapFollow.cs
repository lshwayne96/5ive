/*
 * This script is attached to the camera pointing at the player.
 * The camera knows where the player is by using the currentPlayerRoom
 * from the SetCurrentRoom script.
 */

using UnityEngine;

public class PlayerCameraSnapFollow : MonoBehaviour {

    private Transform target;
    public float damping = 0;
    public float lookAheadFactor = 0;
    public float lookAheadReturnSpeed = 0f;
    public float lookAheadMoveThreshold = 0f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

    private BoxCollider2D roomCollider;

    // Use this for initialization
    private void Start() {
        target = GameObject.FindWithTag("Player").transform;
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    void Update() {
        roomCollider = SetCurrentRoom.currentPlayerRoom.GetComponent<BoxCollider2D>();

        float offsetX = Mathf.Abs(roomCollider.size.x - 18f) / 2;
        float offsetY = Mathf.Abs(roomCollider.size.y - 10f) / 2;

        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget) {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;

        aheadTargetPos = new Vector3(Mathf.Max(aheadTargetPos.x, roomCollider.transform.position.x - offsetX),
            Mathf.Max(aheadTargetPos.y, roomCollider.transform.position.y - offsetY),
            aheadTargetPos.z);

        aheadTargetPos = new Vector3(Mathf.Min(aheadTargetPos.x, roomCollider.transform.position.x + offsetX),
            Mathf.Min(aheadTargetPos.y, roomCollider.transform.position.y + offsetY),
            aheadTargetPos.z);

        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = target.position;
    }
}
