using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIAnimations
{
    public static void YUIMover(GameObject gameObject, Vector2 startPos, float endYPos,float time, LeanTweenType easeType = LeanTweenType.easeOutExpo)
    {
        GameObject go = gameObject;

        go.SetActive(true);
        go.transform.localPosition = startPos;
        
        gameObject.LeanMoveLocalY(endYPos, time).setEase(easeType);
    }

    public static void XUIMover(GameObject gameObject, Vector2 startPos, float endXPos, float time, LeanTweenType easeType = LeanTweenType.easeOutExpo)
    {
        GameObject go = gameObject;

        go.SetActive(true);
        go.transform.localPosition = startPos;
        
        gameObject.LeanMoveLocalX(endXPos, time).setEase(easeType);
    }
}
