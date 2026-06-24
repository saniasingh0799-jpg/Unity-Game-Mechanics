using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIBHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("PNG Setup")]
    public GameObject baseImage;    
    public GameObject hoverImage;   

    [Header("Animation Settings")]
    public float growSize = 1.1f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        ResetButton();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * growSize;
        if (baseImage != null) baseImage.SetActive(false);
        if (hoverImage != null) hoverImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetButton();
    }

    private void ResetButton()
    {
        transform.localScale = originalScale;
        if (baseImage != null) baseImage.SetActive(true);
        if (hoverImage != null) hoverImage.SetActive(false);
    }
}