using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SoundEmitter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool isMouseOn;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOn = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isMouseOn)
        {
            gameObject.transform.DOPunchScale(new Vector3(1f,1f,0f), .2f).SetEase(Ease.OutBack);
            AudioManager.instance.Play("fart");
        }
    }
}
