using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Timer timer; 
    public Canvas canvas;
    public DragDropManager dragDropManager;
    public Transform correspondingSnap;

    private int correspondingSnapIndex;
    private readonly float pieceRotationTime = 0.2f;

    public int id;
    public PieceSlot[] allSnaps;
    public bool IsDragged;
    public bool CanRotate;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 initialPosition;
    public bool IsLocked;

    public bool IsOrientedRight()
    {
        float Epsilon = 0.1f;
        float DesiredRotation = 0.0f;
        return Mathf.Abs(DesiredRotation - transform.rotation.z) < Epsilon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragged = true;
        canvasGroup.blocksRaycasts = false;
        correspondingSnap.SetAsLastSibling();
        dragDropManager.SetOtherDragDropsState(id, false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragged = false;
        canvasGroup.blocksRaycasts = true;
        correspondingSnap.SetSiblingIndex(correspondingSnapIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsLocked)
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CanRotate && !IsLocked && !IsDragged)
        {
            StartCoroutine(Rotate(pieceRotationTime, Vector3.forward * 90.0f));
        }
    }

    private void Start()
    {
        CanRotate = true;
        correspondingSnapIndex = correspondingSnap.GetSiblingIndex();
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(InitialisePositions());
    }

    private IEnumerator InitialisePositions()
    {
        yield return null;
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.position;
    }

    private IEnumerator Rotate(float duration, Vector3 degreesByAngle)
    {
        CanRotate = false;

        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + degreesByAngle);

        for (float t = 0f; t < 1; t += Time.deltaTime / duration)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        transform.rotation = toAngle;

        CanRotate = true;
    }

    public IEnumerator FadeColor(float duration, GameObject obj, float desiredAlpha)
    {
        Image image = obj.GetComponent<Image>();
        Color currentColor = image.color;

        if (currentColor.a == desiredAlpha) yield break;

        float startAlpha = currentColor.a;
        float timeElapsed = 0f;
        float lerpValue;
        float alpha;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            lerpValue = timeElapsed / duration;
            alpha = Mathf.Lerp(startAlpha, desiredAlpha, lerpValue);
            image.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }

        image.color = new Color(currentColor.r, currentColor.g, currentColor.b, desiredAlpha);
        gameObject.SetActive(false);
    }

    public IEnumerator WrongPositionResponse(float duration, Vector3 targetPosition)
    {
        IsDragged = true;
        CanRotate = false;
        GetComponent<Image>().raycastTarget = false;

        Vector3 startPosition = transform.position;

        for (float t = 0f; t < 1; t += Time.deltaTime / duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition;
        dragDropManager.SetOtherDragDropsState(id, true);

        GetComponent<Image>().raycastTarget = !IsLocked;

        IsDragged = false;
        CanRotate = true;
    }
}
