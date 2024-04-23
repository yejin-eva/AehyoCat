using UnityEngine;
using DG.Tweening;

public class CookieManager : MonoBehaviour
{
    
    [SerializeField] private GameObject cookiePrefab;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private RectTransform catRect;

    private float cookieSpawnTime = 3f;
    private float timer = 0f;

    private void Start()
    {
        canvasRect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        UpdateTimer();
        if (timer >= cookieSpawnTime)
        {
            timer = 0f;
            cookieSpawnTime = Random.Range(3f, 10f);
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
        Debug.Log("Cat ate the cookie!");

    }

    private void SpawnAtRandomPosition(GameObject cookie)
    {
        RectTransform cookieRect = cookie.GetComponent<RectTransform>();

        if (cookieRect != null && canvasRect != null)
        {
            float x = UnityEngine.Random.Range(0, canvasRect.sizeDelta.x - cookieRect.sizeDelta.x);
            float y = UnityEngine.Random.Range(0, canvasRect.sizeDelta.y - cookieRect.sizeDelta.y);

            cookieRect.anchoredPosition = new Vector2(x, y);
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