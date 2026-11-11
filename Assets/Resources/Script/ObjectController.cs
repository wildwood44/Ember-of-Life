using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    //private CapsuleCollider2D collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Examine() {
        dialogueManager.EnterDialogueMode();
    }
}
