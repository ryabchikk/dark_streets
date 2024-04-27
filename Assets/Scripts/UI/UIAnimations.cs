using DG.Tweening;
using UnityEngine;

public static class UIAnimations
{

    public static void DOYUIMover(GameObject gameObject, float startYPos, float time = 0.5f,Ease easeType = Ease.OutExpo)
    {
        RectTransform rectTransform;
        gameObject.SetActive(true);

        if (gameObject.GetComponent<RectTransform>()) {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        else {
            return;
        }

        float endYPos = rectTransform.anchoredPosition.y;

        rectTransform.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, startYPos);
        rectTransform.DOAnchorPosY(endYPos, time).SetEase(easeType);
    }

    public static void DOYUIMover(GameObject gameObject, RectTransform startPos, RectTransform endPos, float time = 0.5f, Ease easeType = Ease.OutExpo)
    {
        RectTransform rectTransform;
        gameObject.SetActive(true);

        if (gameObject.GetComponent<RectTransform>()) {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        else {
            return;
        }

        float endYPos = endPos.anchoredPosition.y;
        float startYPos = startPos.anchoredPosition.y;
        
        rectTransform.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, startYPos);
        rectTransform.DOAnchorPosY(endYPos, time).SetEase(easeType);
    }

    public static void DOXUIMover(GameObject gameObject, float startXPos, float time = 0.5f, Ease easeType = Ease.OutExpo)
    {
        RectTransform rectTransform;
        gameObject.SetActive(true);

        if (gameObject.GetComponent<RectTransform>()) {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }
        else {
            return;
        }

        float endXPos = rectTransform.anchoredPosition.x;

        rectTransform.transform.localPosition = new Vector2(startXPos,gameObject.transform.localPosition.y);
        rectTransform.DOAnchorPosX(endXPos, time).SetEase(easeType);
    }
}
