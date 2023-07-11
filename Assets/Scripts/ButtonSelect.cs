using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] public Button button;
    public void OnPointerEnter(PointerEventData eventData)
     {
         button.Select();
     }
}
