using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool playerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            ConversationManager.Instance.StartConversation(myConversation); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; 

            if (ConversationManager.Instance.IsConversationActive) 
            {
                ConversationManager.Instance.EndConversation();
            }
        }
    }

}