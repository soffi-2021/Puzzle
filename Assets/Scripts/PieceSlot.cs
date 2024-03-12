using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceSlot : MonoBehaviour, IDropHandler
{
    public GameObject mainPanel;
    public int Id;
    public float snapDuration;
    public float fadeDuration = 1.0f;
    public float returnDuration; 

    public void OnDrop(PointerEventData eventData)
    {
        var dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
        if (eventData.pointerDrag != null && dragDrop != null && Id == dragDrop.id && dragDrop.IsOrientedRight())
        {
            StartCoroutine(dragDrop.FadeColor(fadeDuration, gameObject, 1.0f));
            StartCoroutine(dragDrop.FadeColor(fadeDuration, eventData.pointerDrag, 0.0f));
            StartCoroutine(dragDrop.WrongPositionResponse(snapDuration, GetComponent<RectTransform>().position));
            dragDrop.IsLocked = true;
            mainPanel.GetComponent<ArchitecturalWorks>().IncrementScore();

            eventData.pointerDrag.GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            StartCoroutine(dragDrop.WrongPositionResponse(returnDuration, dragDrop.initialPosition));
        }
    }
}
