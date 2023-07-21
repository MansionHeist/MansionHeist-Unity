using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListViewManager : MonoBehaviour
{
    public GameObject listItem; // 리스트뷰 아이템
    public RectTransform contentTransform; // 리스트뷰 아이템들이 자식으로 들어갈 Content Transform

    private int rows = 5;
    private int columns = 2;

    private List<string> nicknames; // 서버로부터 받아온 닉네임들을 저장할 리스트

    // Start is called before the first frame update
    void Start()
    {        
        // 임시로 서버에서 닉네임들을 받아온다고 가정
        // 서버로부터 닉네임들을 받아와서 nicknames 리스트에 저장한다고 가정
        nicknames = new List<string>
        {
            "Player1", "Player2", "Player3", "Player4", "Player5",
            "Player6", "Player7", "Player8", "Player9", "Player10"
        };
        CreateListView();
    }

    private void CreateListView()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index >= nicknames.Count)
                    return; // 리스트의 끝까지 생성되었다면 중단

                // 리스트뷰 항목 프리팹 생성
                GameObject item = Instantiate(listItem, contentTransform);

                // 항목의 위치와 크기 조정
                RectTransform itemRect = item.GetComponent<RectTransform>();
                float itemWidth = contentTransform.rect.width / columns;
                float itemHeight = contentTransform.rect.height / rows;
                itemRect.sizeDelta = new Vector2(itemWidth, itemHeight);
                itemRect.anchoredPosition = new Vector2(col * itemWidth, -row * itemHeight);

                // 여기서 필요한 데이터나 UI 요소들을 항목에 채웁니다.
                // 예를 들어, Text 컴포넌트를 가져와서 텍스트를 설정하거나, 이미지를 설정하는 등의 작업을 수행할 수 있습니다.
                TextMeshPro textComponent = item.GetComponentInChildren<TextMeshPro>();
                if (textComponent != null)
                    textComponent.text = nicknames[index];
            }
        }
    }
}
