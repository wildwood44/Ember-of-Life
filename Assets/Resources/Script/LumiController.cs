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
        
        animator.SetFloat("Move X", move.x);
        animator.SetFloat("Move Y", move.y);
        rigidbody2d.linearVelocity = new Vector2(move.x, move.y) * speed;
        //rigidbody2d.MovePosition(rigidbody2d.linearVelocity * Time.fixedDeltaTime);
        //graphics.transform.localPosition = new Vector3(0, 0, 0);
    }
}
