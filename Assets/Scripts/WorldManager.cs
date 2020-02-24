using UnityEngine;
using TMPro;

namespace simplemergegame
{
    public class WorldManager : MonoBehaviour
    {

        [SerializeField]
        private IntVariable score;

        [SerializeField]
        private TextMeshProUGUI textScore;

        [SerializeField]
        private GameObject[] cardFirstPrefabs, cardSecondPrefabs, cardThirdPrefabs;

        private GameObject[,] cards = new GameObject[5,5];

        private int countCards = 0;

        public void AddElement()
        {
            int index = Random.Range(0, cardFirstPrefabs.Length);
            int x = Random.Range(0, cards.GetLength(0));
            int y = Random.Range(0, cards.GetLength(1));
            if (countCards < 25)
            {
                if (cards[x, y] != null)
                {
                    AddElement();
                }
                else
                {
                    cards[x, y] = Instantiate(cardFirstPrefabs[index], new Vector2(x - 2f, y - 2f), Quaternion.identity);
                    ++countCards;
                }
            }
        }

        public void UpgradeCard(int oldX, int oldY, int x, int y)
        {
            GameObject[] gameObjects = null;
            if (x + 2 < 5 && x + 2 >= 0 && y + 2 >= 0 && y + 2 < 5 && cards[x + 2, y + 2] != null && cards[x + 2, y + 2] != cards[oldX + 2, oldY + 2] && cards[x + 2, y + 2].tag == cards[oldX + 2, oldY + 2].tag)
            {
                string tag = cards[x + 2, y + 2].tag;
                if (tag.Substring(0, 1) == "0")
                {
                    gameObjects = cardSecondPrefabs;
                } if (tag.Substring(0, 1) == "1")
                {
                    gameObjects = cardThirdPrefabs;
                }
                if (gameObjects != null)
                {
                    --countCards;
                    Destroy(cards[x + 2, y + 2]);
                    Destroy(cards[oldX + 2, oldY + 2]);
                    cards[oldX + 2, oldY + 2] = null;
                    cards[x + 2, y + 2] = Instantiate(gameObjects[int.Parse(tag.Substring(2))], new Vector2(x, y), Quaternion.identity);
                } else
                {
                    countCards -= 2;
                    Destroy(cards[x + 2, y + 2]);
                    Destroy(cards[oldX + 2, oldY + 2]);
                    cards[x + 2, y + 2] = null;
                    cards[oldX + 2, oldY + 2] = null;
                    score.Value = score.Value + 100;
                    textScore.text = score.Value + "";
                }
            } else
            {
                cards[oldX + 2, oldY + 2].transform.position = new Vector2(oldX, oldY);
            }
        }
    }
}