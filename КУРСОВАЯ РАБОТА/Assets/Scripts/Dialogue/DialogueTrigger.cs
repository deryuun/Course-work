using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerInputManager _inputManager;
    
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        _inputManager = player.GetComponent<PlayerInputManager>();
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (_inputManager.GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else 
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            playerInRange = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.CompareTag("Player")) 
        {
            playerInRange = false;
        }
    }
}