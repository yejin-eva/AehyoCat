using UnityEngine;

public class ClearButton : MonoBehaviour
{
    [SerializeField] TMPro.TMP_InputField inputField;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnClearButtonClicked());
    }
    private void OnDisable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
    }

    private void OnClearButtonClicked()
    {
        inputField.text = "";
    }
}
