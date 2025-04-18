using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueManager))]
public class TalkableNPC : MonoBehaviour
{
    public GameObject DialogueManagerObject;
    public DialogueManager dialogueManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DialogueManagerObject != null)
        {
            dialogueManager = DialogueManagerObject.GetComponent<DialogueManager>();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueManager != null)
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                dialogueManager.StartDialogue();
                print("Started");
            }
        }   
        dialogueManager.SetDialoguePart();
    }
}
