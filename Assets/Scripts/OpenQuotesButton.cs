using UnityEngine;
using DG.Tweening;

public class OpenQuotesButton : MonoBehaviour
{
    [SerializeField] GameObject quoteBubble;
    
    UnityEngine.UI.Button openQuotesButton;
    
    private void OnEnable()
    {
        openQuotesButton = GetComponent<UnityEngine.UI.Button>();
        openQuotesButton.onClick.AddListener(() => OnOpenQuotesButtonClicked());
    }
    private void OnDisable()
    {
        openQuotesButton.onClick.RemoveAllListeners();
    }
    private void OnOpenQuotesButtonClicked()
    {
        quoteBubble.SetActive(true);
        this.gameObject.SetActive(false);
    }

    

    
}
