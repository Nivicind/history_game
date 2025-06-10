using UnityEngine;
using UnityEngine.UI; // Để truy cập lớp Button
using UnityEngine.EventSystems; // Để truy cập EventSystem

public class RestartButton : MonoBehaviour
{
    public Button targetButton;
    public KeyCode activationKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            if (targetButton != null && targetButton.gameObject.activeInHierarchy && targetButton.interactable)
            {
                EventSystem.current.SetSelectedGameObject(targetButton.gameObject);
                targetButton.onClick.Invoke();
                EventSystem.current.SetSelectedGameObject(null); 
            }
        }
    }
}