using UnityEngine;

public class GhostCell : MonoBehaviour
{
    [Header("Visual Components")]
    public SpriteRenderer bodyRenderer;

    [Header("4 Border Edges")]
    public GameObject borderTop;
    public GameObject borderBottom;
    public GameObject borderLeft;
    public GameObject borderRight;

    public void SetBorders(bool top, bool bottom, bool left, bool right)
    {
        if (borderTop != null) borderTop.SetActive(top);
        if (borderBottom != null) borderBottom.SetActive(bottom);
        if (borderLeft != null) borderLeft.SetActive(left);
        if (borderRight != null) borderRight.SetActive(right);
    }
}