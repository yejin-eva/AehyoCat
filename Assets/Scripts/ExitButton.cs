using CodeMonkey.Utils;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button button;
    
    private void OnEnable()
    {
        button.onClick.AddListener(() => OnExitButtonClicked());
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    
}
