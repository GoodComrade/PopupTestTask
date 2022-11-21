using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopupAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private CanvasGroup content;
    [SerializeField] private CanvasGroup tooltip;
    [SerializeField] private CanvasGroup button;

    /*Unfortinalty interaction of DOTween asset with
    TextMeshPro is avaluable only in paid version of asset*/
    [SerializeField] private CanvasGroup headerText;

    [SerializeField] private float animationDuration = 1f;

    private RectTransform tooltipRect;

    //Start sizes of ui elements
    private float containerWidthStart = 1118;
    private float containerHeightStart = 85;
    private float tooltipHeightStart = 110;

    //Size of popup container without tooltip
    private float defaultContainerHeight = 590;
    //Size of popup container with tooltip
    private float containerHeightTooltip = 720;

    //Popup container size to expand where tooltip is expanding
    private float containerTooltipExpand = 45;

    private Transform[] tooltipTexts;

    private int activateNext = 1;
    void Start()
    {
        tooltipRect = tooltip.GetComponent<RectTransform>();
        tooltipTexts = new Transform[tooltip.transform.childCount];
        for (int i = 0; i < tooltipTexts.Length; i++)
        {
            tooltipTexts[i] = tooltip.transform.GetChild(i);
            if(i > 0)
                tooltipTexts[i].gameObject.SetActive(false);
        }
        content.alpha = 0f;
        tooltip.alpha = 0f;
        button.alpha = 0f;
        headerText.alpha = 0f;


        tooltipRect.sizeDelta = new Vector2(tooltipRect.rect.width, tooltipHeightStart);
        container.sizeDelta = new Vector2(0, 0);

        container.DOSizeDelta(new Vector2(containerWidthStart, containerHeightStart), animationDuration, false).OnComplete(FadeHeaderText);
    }
    void FadeHeaderText()
    {
        headerText.DOFade(1, animationDuration).OnComplete(ExpandPopup);
    }
    void ExpandPopup()
    {
        container.DOSizeDelta(new Vector2(container.rect.width, defaultContainerHeight), animationDuration, false).OnComplete(ShowContent);
    }
    void ShowContent()
    {
        content.DOFade(1, animationDuration).OnComplete(ShowButton);
    }
    void ShowButton()
    {
        button.DOFade(1, animationDuration);
    }
    public void ExpandContentTooltip()
    {
        if (tooltip.alpha < 0.1)
            container.DOSizeDelta(new Vector2(container.rect.width, containerHeightTooltip), animationDuration, false).OnComplete(ShowTooltip);
        else
            ExpandTooltip();
    }
    void ShowTooltip()
    {
        tooltip.DOFade(1, animationDuration);
    }
    void ExpandTooltip()
    {
        if (activateNext < tooltipTexts.Length)
        {
            tooltipRect.DOSizeDelta(new Vector2(tooltipRect.rect.width, tooltipRect.rect.height + containerTooltipExpand), animationDuration, false).OnComplete(ActivateAditionalText);
            container.DOSizeDelta(new Vector2(container.rect.width, container.rect.height + containerTooltipExpand), animationDuration, false);
        }
    }

    void ActivateAditionalText()
    {
        if (activateNext < tooltipTexts.Length)
        {
            tooltipTexts[activateNext].gameObject.SetActive(true);
            activateNext++;
        }
    }
}
