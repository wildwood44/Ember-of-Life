using UnityEngine;
using UnityEngine.InputSystem;

public class Script : EntityController
{
    public InputAction MoveAction;
    private Vector2 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Alder
        move = MoveAction.ReadValue<Vector2>();
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            moveDirection.Set(move.x, move.y);
      	    moveDirection.Normalize();
        }
        Debug.Log("Move X: " + move.x + "  Move Y: " + move.y);
        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    void FixedUpdate()
    {
        //Knockback
        /*if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            rigidbody2d.MovePosition(rigidbody2d.position + knockbackDirection * knockbackForce * Time.fixedDeltaTime);
            return;
        }*/
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
