using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text messageText;

    private void Start()
    {
        var shared = GameManager.Instance.SharedGameState;

        if (!string.IsNullOrEmpty(shared.GetMessage()))
        {
            messageText.text = shared.GetMessage();

            foreach (var card in shared.GetRewards())
            {
                Debug.Log($"報酬カード：{card.name}");
            }

            shared.Clear(); // 表示後に初期化
        }
        else
        {
            messageText.text = "ダンジョンを選んでください";
        }
    }

    public void StartDungeon1Normal()
    {
        var deck = new DungeonDeck();
        int seed = System.DateTime.Now.Millisecond;
        deck.Initialize(seed);

        // 仮のカードを追加（masterCardListから）
        var masterCards = GameManager.Instance.masterCardList;

        if (masterCards.Count >= 3)
        {
            deck.AddCard(masterCards[0]);
            deck.AddCard(masterCards[1]);
            deck.AddCard(masterCards[2]);
        }
        else
        {
            // デバッグ用：名前だけ仮設定されたカードを生成
            for (int i = 0; i < 10; i++)
            {
                var dummy = new CardBase
                {
                    id = $"TEST_{i}",
                    name = $"仮カード{i + 1}",
                    description = $"これは仮のカードです{i + 1}",
                    type = "buff",
                    cost = 1,
                    rarity = 1
                };
                deck.AddCard(dummy);
            }
        }

        // 仮のボスカード追加
        var bossCard = new CardBase
        {
            id = "BOSS_CARD",
            name = "ボスカード",
            description = "これは仮のボスです",
            type = "enemy",
            cost = 3,
            rarity = 5
        };
        deck.SetBossCard(bossCard);

        //GameManager.Instance.SharedGameState.SetDeck(deck);
        SceneManager.LoadScene("DungeonScene");
    }
}
