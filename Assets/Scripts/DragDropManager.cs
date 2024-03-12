using UnityEngine;
using UnityEngine.UI;

public class DragDropManager : MonoBehaviour
{
    public DragDrop[] allDragDrops;

    public void SetOtherDragDropsState(int exceptId, bool state)
    {
        foreach (var element in allDragDrops)
        {
            if (!element.IsLocked && element.id != exceptId)
            {
                element.GetComponent<Image>().raycastTarget = state;
            }
        }
    }

    public void HandleTimeExpired()
    {
        DisableRaycastTarget();
    }

    private void DisableRaycastTarget()
    {
        foreach(var element in allDragDrops)
        {
            element.GetComponent<Image>().raycastTarget = false;
            element.IsLocked = true;
        }
    }
}
