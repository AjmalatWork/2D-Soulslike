using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashTime;
    public float dashSpeed;
    public float dashCooldown;

    [NonSerialized] public bool isDashing;

    private bool dashInput;
    private bool canDash;    
    private bool isWalled;    
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    private GroundCheck groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        groundCheck = GetComponent<GroundCheck>();
        canDash = true;
    }

    public void HandleDash()
    {
        dashInput = InputManager.Instance.GetDashInput();
        isWalled = groundCheck.IsWalled();

        // Cancel dash if player hits a wall
        if(isWalled && isDashing)
        {
            isDashing = false;
        }

        if (dashInput && canDash)
        {
            StartCoroutine(Dash());            
        }        
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;
        rb.velocity = new Vector2 (dashSpeed * transform.localScale.x, 0f);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;        
    }
}