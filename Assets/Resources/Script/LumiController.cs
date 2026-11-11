using UnityEngine;
using UnityEngine.InputSystem;

public class LumiController : EntityController
{
    public InputAction MoveAction;
    public InputAction InteractAction;
    private Vector2 move;
    private CapsuleCollider2D collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        collider = GetComponent<CapsuleCollider2D>();
        MoveAction.Enable();
        InteractAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractAction.WasPressedThisFrame() && GameManager.CurrentState == GameState.IsTalking)
        {
            Interact();
        }
        if (GameManager.IsFrozen)
        {
            move.x = 0;
            move.y = 0;
            return;
        }
        //Move Lumi
        move = MoveAction.ReadValue<Vector2>();
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
        {
            moveDirection.Set(move.x, move.y);
      	    moveDirection.Normalize();
        }
        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        if (InteractAction.WasPressedThisFrame())
        {
            Interact();
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
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void Interact()
    {
        Debug.Log("Interact");
        Vector2 origin = rigidbody2d.position;
        int mask = LayerMask.GetMask("NPC", "Object");
        Collider2D[] hits;

        if (collider != null)
        {
            Vector2 capsuleCenter = collider.bounds.center;
            Vector2 capsuleSize = new Vector2(collider.bounds.size.x, collider.bounds.size.y) + new Vector2(0.8f, 1.0f);
            hits = Physics2D.OverlapCapsuleAll(capsuleCenter, capsuleSize, CapsuleDirection2D.Vertical, 0f, mask);
            
            Debug.Log("With something " + hits);
        }
        else
        {
            hits = Physics2D.OverlapCircleAll(origin + Vector2.up * 0.3f, 1.2f, mask);
        }

        if (hits == null || hits.Length == 0) return;

        foreach (Collider2D hit in hits)
        {
            //OBJECT
            if (hit.TryGetComponent(out ObjectController obj))
            {
                Debug.Log("Ping");
                HandleObjectInteraction(obj);
                return;
            }
        }
    }

    void HandleObjectInteraction(ObjectController obj)
    {
        obj.Examine();
    }
}
