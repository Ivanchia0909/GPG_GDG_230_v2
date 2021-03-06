﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
   
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (eventData.pointerDrag == null)
            return;

        DraggableCard d = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (d != null)
            d.placeHolderParent = this.transform;

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (eventData.pointerDrag == null)
            return;

        DraggableCard d = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (d != null && d.placeHolderParent == this.transform)
            d.placeHolderParent = d.parentToReturnTo;

    }

    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log(eventData.pointerDrag.name + "was dropped on " + gameObject.name);

        DraggableCard d = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (d != null)
            d.parentToReturnTo = this.transform;

    }

    
  
}
