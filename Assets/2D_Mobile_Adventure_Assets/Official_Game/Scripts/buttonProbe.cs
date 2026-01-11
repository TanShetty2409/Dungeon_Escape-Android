using UnityEngine;
using UnityEngine.EventSystems;

public class buttonProbe : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HOVER OK");
    }
}
