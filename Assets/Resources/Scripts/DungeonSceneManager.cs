using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DungeonSceneManager : MonoBehaviour
{
    [Header("操作ボタン")]
    public Button drawButton;
    public Button clearButton;
    public Button returnButton;
    public Button soundDisplayButton;
    public Button soundPlayTestButton;

    [Header("カード表示UI")]
    public CardButtonUI[] cardButtons; // 左:0, 中央:1, 右:2

    [Header("共通メッセージ表示")]
    public TMP_Text messageText;

    private DungeonDeck deck;

    [Header("獲得カード表示エリア")]
    public Transform stackArea;             // Horizontal Layout Groupなど
    public GameObject stackCardPrefab;      // CardButtonUIプレハブ
    private List<CardBase> acquiredCards = new();
    private void Start()
    {
        drawButton.onClick.AddListener(ShowNextDraw);
        clearButton.onClick.AddListener(TriggerClear);
        returnButton.onClick.AddListener(ReturnToTitle);
        soundDisplayButton.onClick.AddListener(() => GameManager.Instance.ToggleSoundPanel());
        soundPlayTestButton.onClick.AddListener(() => GameManager.Instance.BGM1ButtonEvent());  

        deck = GameManager.Instance.SharedGameState.GetDeck();

        if (deck == null)
        {
            deck = CreateDummyDeck();
            GameManager.Instance.SharedGameState.SetDeck(deck);
        }

        ShowNextDraw();
    }

void ShowNextDraw()
    {
        var drawn = deck.Draw(3);
        if (drawn.Count == 0)
        {
            Debug.Log("山札が空です");
            return;
        }

        for (int i = 0; i < cardButtons.Length; i++)
        {
            if (i < drawn.Count)
            {
                var card = drawn[i];
                int index = i;
                cardButtons[i].SetCard(card, OnCardSelected );
                cardButtons[i].gameObject.SetActive(true);
            }
            else
            {
                cardButtons[i].gameObject.SetActive(false);
            }
        }
    }
    void OnCardSelected(CardBase selected)
    {
        Debug.Log($"選択：{selected.name} "+selected.selectedOption );

        // スタックに追加してUI表示
        acquiredCards.Add(selected);
        ShowStackCard(selected);

        // ボスなら終了
        if (selected.type == "boss")
        {
            GameManager.Instance.SharedGameState.SetReward(acquiredCards, "ボス撃破！報酬獲得！");
            ReturnToTitle();
            return;
        }

        // 次のドロー
        ShowNextDraw();
    }
    void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    void ShowStackCard(CardBase card)
    {
        var obj = Instantiate(stackCardPrefab, stackArea);
        var ui = obj.GetComponent<CardButtonUI>();
        if (ui != null)
        {
            ui.SetCard(card, null); // 選択処理なし
        }
    }
    void TriggerClear()
    {
        GameManager.Instance.SharedGameState.SetReward(new List<CardBase>(), "テスト報酬");
            ReturnToTitle();
    }

    private DungeonDeck CreateDummyDeck()
    {
        var deck = new DungeonDeck();
        deck.Initialize(System.DateTime.Now.Millisecond);

        // ① カード一覧を配列で定義（最初がボス）
        CardBase[] dummyCards = new CardBase[]
        {
            new CardBase
            {
                id = "boss_1",
                name = "仮ボスカード",
                description = "これはボスです",
                type = "boss",
                cardImage = "Images/monster4",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/monster4"
            },
            new CardBase
            {
                id = "add_seven_symbol",
                name = "７ボーナス図柄追加",
                description = "これはボスです",
                type = "symbol",
                cardImage = "Images/slotsymbol1:slotsymbol1_0",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotsymbol1:slotsymbol1_0"
            },
            new CardBase
            {
                id = "add_cherry_symbol",
                name = "チェリー図柄追加",
                description = "リールにチェリー追加します",
                type = "symbol",
                cardImage = "Images/slotsymbol1:slotsymbol1_1",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotsymbol1:slotsymbol1_1"
            },  
            new CardBase
            {
                id = "add_bell_symbol",
                name = "ベル図柄追加",
                description = "リールにベルを等間隔に追加します",
                type = "symbol",
                cardImage = "Images/slotsymbol1:slotsymbol1_2",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotsymbol1:slotsymbol1_2"
            },  
            new CardBase
            {
                id = "add_bell_flag",
                name = "ベル成立役",
                description = "ベルの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_0",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_0",
                selectableOptions = new List<string> { "安定型", "払出型", "SP強化型"}
            },  
            new CardBase
            {
                id = "add_replay_flag",
                name = "リプレイ成立役",
                description = "リプレイの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_2",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_2",
                selectableOptions = new List<string> { "SP強化型" }
            }, 
            new CardBase
            {
                id = "add_cherry_flag",
                name = "チェリー成立役",
                description = "チェリーの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_1",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_1",
                selectableOptions = new List<string> { "SP強化型", "払出型" }
            }, 
            new CardBase
            {
                id = "add_suika_flag",
                name = "スイカ成立役",
                description = "スイカの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_3",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_3",
                selectableOptions = new List<string> { "SP強化型", "払出型" }
            }, 
            new CardBase
            {
                id = "add_big_flag",
                name = "ビッグボーナス成立役",
                description = "ビッグボーナスの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_4",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_4",
                selectableOptions = new List<string> { "回数強化型", "安定型" }
            }, 
            new CardBase
            {
                id = "add_reg_flag",
                name = "レギュラーボーナス成立役",
                description = "レギュラーボーナスの成立役を取得・強化します",
                type = "flag",
                cardImage = "Images/slotflag1:slotflag1_5",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background2",
                detailImage = "Images/slotflag1:slotflag1_5",
                selectableOptions = new List<string> { "安定型" }
            }, 
            new CardBase
            {
                id = "Enemy_1",
                name = "ヒュドラ",
                description = "これは仮のカードです1",
                type = "enemy",
                cardImage = "Images/monster1",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background1",
                detailImage = "Images/monster1"
            },
            new CardBase
            {
                id = "Enemy_2",
                name = "ドラゴン",
                description = "これは仮のカードです2",
                type = "enemy",
                cardImage = "Images/monster2",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background1",
                detailImage = "Images/monster2"
            },
            new CardBase
            {
                id = "Enemy_3",
                name = "リヴァイアサン",
                description = "これは仮のカードです3",
                type = "enemy",
                cardImage = "Images/monster3",
                cardFlameImage = "Images/card_flame1",
                backgroundImage = "Images/background1",
                detailImage = "Images/monster3"
            },
            new CardBase
            {
                id = "Item_1",
                name = "マッチョ",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem1",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem1"
            },
            new CardBase
            {
                id = "Item_2",
                name = "オーラ爆発",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem2",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem2"
            },
            new CardBase
            {
                id = "Item_3",
                name = "赤オーラ",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem3",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem3"
            },
            new CardBase
            {
                id = "Item_4",
                name = "赤ポーション",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem4",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem4"
            },
            new CardBase
            {
                id = "Item_5",
                name = "緑ポーション",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem5",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem5"
            },
            new CardBase
            {
                id = "Item_6",
                name = "毒ポーション",
                description = "これは仮のカードです3",
                type = "buff",
                cardImage = "Images/eventItem6",
                cardFlameImage = "Images/card_flame2",
                backgroundImage = "Images/background2",
                detailImage = "Images/eventItem6"
            }
        };

        // ② ボスカード（先頭）
        deck.SetBossCard(dummyCards[0]);

        // ③ 通常カード（2番目以降）
        for (int i = 1; i < dummyCards.Length; i++)
        {
            deck.AddCard(dummyCards[i]);
        }
        // ③ 通常カード（2番目以降）
        for (int i = 1; i < dummyCards.Length; i++)
        {
            deck.AddCard(dummyCards[i]);
        }

        return deck;
    }
}
