using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public DialogueElement[] DialogueElements;
    public bool IsTalking;
    public Text SpeakerText, ContentText;
    public Image SpeakerImage;
    public GameObject GeneralPanel;
    [SerializeField]
    private int ElementIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    //Bu kod baþka kodlarda kullanýlacaktýr ve GetComponent ile hangi scripting çaðýrýlacaðý belirlenecektir. Örnek: Obj.GetComponent<DialogueManager>().StartDialogue();
    public void StartDialogue()
    {
        IsTalking = true;
        GeneralPanel.SetActive(IsTalking);
        ElementIndex = 0;
    }
    public void SetDialoguePart()
    {
        if (IsTalking)
        {
            GeneralPanel.SetActive(IsTalking);

            if (ElementIndex < DialogueElements.Length)
            {
                SpeakerText.text = DialogueElements[ElementIndex].Speaker;
                ContentText.text = DialogueElements[ElementIndex].Content;
                SpeakerImage.sprite = DialogueElements[ElementIndex].SpeakerPicture;
            }
            else
            {
                EndDialogue();
            }
            if (Input.GetMouseButtonDown(0))
            {
                ElementIndex++;
            }
        }
    }
    //Bu kod diyaloðu bitirir ve yeni bir diyaloðun baþlamasýna zemin hazýrlar.
    public void EndDialogue()
    {
        IsTalking = false;
        GeneralPanel.SetActive(IsTalking);
        ElementIndex = 0;
    }
}

[System.Serializable]

public class DialogueElement
{
    public Sprite SpeakerPicture;
    [Tooltip("The name or nickname of the speaker.")]
    public string Speaker;
    [TextArea]
    public string Content;
}

