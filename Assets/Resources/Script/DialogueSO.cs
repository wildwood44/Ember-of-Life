using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/DialogueNode")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Conditional Requirements (Optional)")]
    public ActorSO[] requiredNPCs;
    //public HeroState[] requiredStates;
    public bool IsConditionMet()
    {
        if (requiredNPCs.Length > 0)
        {
            foreach (var npc in requiredNPCs)
            {
                
            }
        }
        /*if (requiredStates.Length > 0)
        {
            //HeroController hero = FindAnyObjectByType<HeroController>();
            foreach (var state in requiredStates)
            {
                if(hero.GetCurrentState() != state)
                {
                    return false;
                }
            }
        }*/
        return true;
    }
}

[System.Serializable]
public class DialogueLine {
    public ActorSO speaker;
    [TextArea(3,5)] public string text;
}

[System.Serializable]
public class DialogueOption {
    public string optionText;
    public DialogueSO nextDialogue;
}

[System.Serializable]
public class StateOption {
    public DialogueSO nextDialogue;
}