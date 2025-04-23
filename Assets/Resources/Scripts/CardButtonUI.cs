using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq; 

public class CardButtonUI : MonoBehaviour
{
    [Header("オプション選択ボタン（最大4つ）")]
    public Button[] optionButtons;  // インスペクターでアタッチ（Button配列）
    public TextMeshProUGUI[] optionButtonLabels; // 各ボタンのラベル用
    private Action<CardBase> onSelectCallback;
    private CardBase cardData;

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

    public void SetCard(CardBase card, Action<CardBase> onSelectCallback)
    {
        cardData = card;
        this.onSelectCallback = onSelectCallback;

        nameText.text = card.name;
        descText.text = card.description;

        //cardImage.sprite = LoadSprite(card.cardImage);
        cardImage.sprite = CardBase.LoadSpriteByPath(card.cardImage);
        //cardFlameImage.sprite = LoadSprite(card.cardFlameImage);
        cardFlameImage.sprite = CardBase.LoadSpriteByPath(card.cardFlameImage);
        //backgroundImage.sprite = LoadSprite(card.backgroundImage);
        backgroundImage.sprite = CardBase.LoadSpriteByPath(card.backgroundImage);

        // ✅ 全ボタン初期非表示 + リスナー除去
        selectButton.onClick.RemoveAllListeners();
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(false);
            optionButtons[i].onClick.RemoveAllListeners();
        }
        // カード選択
        if (card.selectableOptions == null || card.selectableOptions.Count <= 1)
        {
            card.selectedOption = card.selectableOptions.FirstOrDefault();
            selectButton.gameObject.SetActive(true);
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => onSelectCallback?.Invoke(card));
        }
        else
        {
            // 複数選択肢がある → ボタンを個別に表示
            selectButton.gameObject.SetActive(false);

            for (int i = 0; i < Mathf.Min(card.selectableOptions.Count, optionButtons.Length); i++)
            {
                string option = card.selectableOptions[i];
                optionButtonLabels[i].text = option;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() =>
                {
                    card.selectedOption = option;
                    onSelectCallback?.Invoke(card);
                });
            }
        }
    }

    private Sprite LoadSprite(string resourcePath)
    {
        if (string.IsNullOrEmpty(resourcePath)) return null;

        var sprite = Resources.Load<Sprite>(resourcePath);
        if (sprite == null)
        {
            Debug.LogWarning($"画像が見つかりません: {resourcePath}");
        }
        return sprite;
    }
    void ShowDetailModal()
    {
        if (detailModal != null)
        {
            detailTitleText.text = cardData != null ? cardData.name : "--";
            detailDescText.text = cardData != null ? cardData.description : "カードが設定されていません";
            detailImage.sprite = cardData != null ? LoadSprite(cardData.detailImage) : null;

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
