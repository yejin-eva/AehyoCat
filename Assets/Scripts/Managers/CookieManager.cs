using UnityEngine;
using DG.Tweening;
using System;

public class CookieManager : MonoBehaviour
{
    public Action OnAteCookie;
    
    [SerializeField] private GameObject cookiePrefab;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform catRect;

    private float cookieSpawnTime = 3f;
    private float timer = 0f;

    private void Update()
    {
        UpdateTimer();
        if (timer >= cookieSpawnTime)
        {
            timer = 0f;
            cookieSpawnTime = UnityEngine.Random.Range(3f, 10f);
            SpawnCookie();
        }
    }

    private void SpawnCookie()
    {
        GameObject cookie = Instantiate(cookiePrefab, this.transform);

        cookie.GetComponent<DragComponent>().endDrag += EndCookieDrag;

        SpawnAtRandomPosition(cookie);
    }

    private void EndCookieDrag(DragComponent cookieComponent)
    {
        bool isNearCat = RectTransformUtility.RectangleContainsScreenPoint(catRect, cookieComponent.transform.position);
        if (isNearCat)
        {
            EatCookie(cookieComponent);
        }
    }

    private void EatCookie(DragComponent cookieComponent)
    {
        cookieComponent.transform.DOScale(transform.localScale * 0.5f, 0.3f).SetEase(Ease.InBack).OnComplete(() => Destroy(cookieComponent.gameObject));
        
        OnAteCookie?.Invoke();
    }

    private void SpawnAtRandomPosition(GameObject cookie)
    {
        RectTransform cookieRect = cookie.GetComponent<RectTransform>();

        if (cookieRect != null && canvasRect != null)
        {
            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;

            float x = UnityEngine.Random.Range(cookieRect.sizeDelta.x / 2, canvasWidth - cookieRect.sizeDelta.x / 2);
            float y = UnityEngine.Random.Range(cookieRect.sizeDelta.y / 2, canvasHeight - cookieRect.sizeDelta.y / 2);
            
            // Set the anchored position of the cookie within the canvas
            cookieRect.anchoredPosition = new Vector2(x - canvasWidth / 2, y - canvasHeight / 2);
        }
        else
        {
            Debug.LogError("Error: RectTransform not found on cookie or canvasRect not assigned.");
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}
