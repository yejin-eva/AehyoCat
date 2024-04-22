using UnityEngine;

public class ExitButton : MonoBehaviour
{
    private UnityEngine.UI.Button exitButton ;
    
    private void OnEnable()
    {
        exitButton = GetComponent<UnityEngine.UI.Button>();
        exitButton.onClick.AddListener(() => OnExitButtonClicked());
    }
    private void OnDisable()
    {
        exitButton.onClick.RemoveAllListeners();
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    
}
