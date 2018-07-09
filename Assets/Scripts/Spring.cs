﻿/*
 * This scripts allows a gameObject that comes into contact
 * with the attached gameObject to bounce on it.
 * The bouncing gameObject will experience reduced bounces until
 * it stops bouncing.
 */

using UnityEngine;

public class Spring : MonoBehaviour {

    // Exposes both springForce and velocityCap to the editor
    // Multiplier for how high spring will bounce
    public float springForce;
    // Maximum velocity spring should allow
    public float velocityCap;

    void OnTriggerEnter2D(Collider2D collision) {
        Rigidbody2D rbody = collision.GetComponent<Rigidbody2D>();
        // Get downward component of velocity
        float verticalSpeed = Vector2.Dot(rbody.velocity, Vector2.down);

        // Only bounce if object was falling
        if (verticalSpeed > 0) {
            // Cap the velocity
            float bounceVelocity = Mathf.Min(verticalSpeed * springForce, velocityCap);
            rbody.velocity = Vector2.up * bounceVelocity;
        }
    }
}
