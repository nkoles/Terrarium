using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;
    private static TooltipUI instance;
    [SerializeField] private Transform[] parents;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        tooltipText = backgroundRectTransform.GetChild(0).GetComponent<TextMeshProUGUI>();

        HideTooltip();
    }

    private void Update()
    {
        /*Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, myCamera, out localPoint);
        transform.localPosition = localPoint;*/
        transform.position = new Vector2(Input.mousePosition.x + offset.x, Input.mousePosition.y + offset.y);
    }

    public void ShowTooltip(string tooltipString)
    {
        transform.localScale = Vector3.one;
        Debug.Log("Showtooltip");
        gameObject.SetActive(true);
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = "";
        backgroundRectTransform.gameObject.SetActive(true);

        // Update tooltip size
        tooltipText.text = tooltipString;
        float textPaddingSize = 4;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2, tooltipText.preferredHeight + textPaddingSize * 2);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);
        backgroundRectTransform.gameObject.SetActive(false);
    }

    public void ChangeParents(int parentIndex)
    {
        transform.SetParent(parents[parentIndex]);
    }
}
