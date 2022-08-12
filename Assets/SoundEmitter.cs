using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SoundEmitter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Mouse detected by: {name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Mouse exited from: {name}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Mouse clicked");
        gameObject.transform.DOPunchScale(new Vector3(1f,1f,0f), .2f).SetEase(Ease.OutBack);;
    }
}
