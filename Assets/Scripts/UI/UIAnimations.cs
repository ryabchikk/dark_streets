using DG.Tweening;
using UnityEngine;

public static class UIAnimations
{
    public static void YUIMover(GameObject gameObject, float startYPos, float time = 0.5f, LeanTweenType easeType = LeanTweenType.easeOutExpo)
    {
        GameObject go = gameObject;
        float endYPos = go.transform.localPosition.y;

        go.SetActive(true);
        go.transform.localPosition = new Vector2(go.transform.localPosition.x, startYPos);
        
        gameObject.LeanMoveLocalY(endYPos, time).setEase(easeType);
    }

    public static void XUIMover(GameObject gameObject, float startXPos, float time =0.5f, LeanTweenType easeType = LeanTweenType.easeOutExpo)
    {
        GameObject go = gameObject;
        float endXPos = go.transform.localPosition.x;
        
        go.SetActive(true);
        go.transform.localPosition = new Vector2(startXPos, go.transform.localPosition.y);
        
        gameObject.LeanMoveLocalX(endXPos, time).setEase(easeType);
    }
    /*
    public static void XYZScaler(GameObject gameObject, float scaleParametr)
    {
        GameObject go = gameObject;
        Vector3 defaultScaleGameObject = new Vector3(go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z);
        Vector3 resultScale = new 
        go.LeanScale(new Vector3(defaultScaleGameObject.x+ scaleParametr,))
    }*/

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
