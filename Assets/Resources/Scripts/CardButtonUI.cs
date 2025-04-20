using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardButtonUI : MonoBehaviour
{
    [Header("カード基本UI")]
    public Image backgroundImage;
    public Image cardImage;
    public Image cardFlameImage;
    public TMP_Text nameText;
    public TMP_Text descText;
    public Button selectButton;
    public Button infoButton; // 「？」ボタン

    [Header("カード詳細表示UI（モーダル）")]
    public GameObject detailModal;
    public TMP_Text detailTitleText;
    public TMP_Text detailDescText;
    public Image detailImage;
    public Button closeDetailButton; // 背景などを押すことで閉じる

    private CardBase cardData;
    private Action onSelect;

    private void Awake()
    {
        if (detailModal != null)
        {
            detailModal.SetActive(false);
        }

        // モーダル閉じる処理
        if (closeDetailButton != null)
        {
            closeDetailButton.onClick.RemoveAllListeners();
            closeDetailButton.onClick.AddListener(CloseDetailModal);
        }

        // 「？」ボタン
        if (infoButton != null)
        {
            infoButton.onClick.RemoveAllListeners();
            infoButton.onClick.AddListener(ShowDetailModal);
        }
    }

    public void SetCard(CardBase card, Action onSelectCallback)
    {
        cardData = card;
        onSelect = onSelectCallback;

        nameText.text = card.name;
        descText.text = card.description;
        cardImage.sprite = card.icon;

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onSelect?.Invoke());
    }

    void ShowDetailModal()
    {
        if (detailModal != null)
        {
            detailTitleText.text = cardData != null ? cardData.name : "--";
            detailDescText.text = cardData != null ? cardData.description : "カードが設定されていません";
            detailImage.sprite = cardData != null ? cardData.icon : null;

            detailModal.SetActive(true);
        }
    }

    public void CloseDetailModal()
    {
        if (detailModal != null)
        {
            detailModal.SetActive(false);
        }
    }
}
