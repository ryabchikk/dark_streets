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
}
