using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] protected TextAsset inkJSON;
    //[SerializeField] private TextAsset loadGlobalsJSON;
    public static DialogueManager Instance { get; private set; }
    //https://www.youtube.com/shorts/is02nCB2hww
    private Queue<string> sentences;
    public static DialogueManager instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public GameObject dialogueBox;
    //public TMP_Text dialogueText;
    public TextMeshProUGUI dialogueText;

    public bool isDialogueActive;

    //public DialogueSO currentDialogue;
    private int dialogueIndex;
    private Story currentStory;
    public  bool dialogueIsPlaying  { get; private set; }
    private DialogueVariable dialogueVariables;

    public LumiController lumi;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        //canvasGroup.alpha = 0;
        //canvasGroup.interactable = false;
        //canvasGroup.blocksRaycasts = false;
        
        dialogueBox.SetActive(false);
        currentStory = new Story(inkJSON.text);
        //dialogueVariables = new DialogueVariable(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    public void Start() {
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        //sentences = new Queue<string>();
    }

    private void Update() {
        if(!dialogueIsPlaying) {
            return;
        }
        if(lumi.InteractAction.WasPressedThisFrame()) {
            ContinueStory();
        }
    }

    public void SetInk(TextAsset inkJSON)
    {
        this.inkJSON = inkJSON;
    }

    public void EnterDialogueMode() {
        GameManager.CurrentState = GameState.IsTalking;
        currentStory = new Story(inkJSON.text);
        //currentStory.variablesState["give"] = heroStatus;
        dialogueIsPlaying = true;
        dialogueBox.SetActive(true);
        //dialogueVariables.StartListening(currentStory);
        //Debug.Log("Dialogue started " + currentStory.Continue());
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);
        GameManager.CurrentState = GameState.Playing;
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory() {
        if(currentStory.canContinue) {
            string nextline = currentStory.Continue();
            if(nextline.Equals("") && !currentStory.canContinue) {
                Debug.Log("Dialogue ended");
                StartCoroutine(ExitDialogueMode());
            }
            
            dialogueText.text = nextline;
        } else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
}
