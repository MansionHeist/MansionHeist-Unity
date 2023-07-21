using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListViewManager : MonoBehaviour
{
    public GameObject listItem; // ����Ʈ�� ������
    public RectTransform contentTransform; // ����Ʈ�� �����۵��� �ڽ����� �� Content Transform

    private int rows = 5;
    private int columns = 2;

    private List<string> nicknames; // �����κ��� �޾ƿ� �г��ӵ��� ������ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {        
        // �ӽ÷� �������� �г��ӵ��� �޾ƿ´ٰ� ����
        // �����κ��� �г��ӵ��� �޾ƿͼ� nicknames ����Ʈ�� �����Ѵٰ� ����
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
                    return; // ����Ʈ�� ������ �����Ǿ��ٸ� �ߴ�

                // ����Ʈ�� �׸� ������ ����
                GameObject item = Instantiate(listItem, contentTransform);

                // �׸��� ��ġ�� ũ�� ����
                RectTransform itemRect = item.GetComponent<RectTransform>();
                float itemWidth = contentTransform.rect.width / columns;
                float itemHeight = contentTransform.rect.height / rows;
                itemRect.sizeDelta = new Vector2(itemWidth, itemHeight);
                itemRect.anchoredPosition = new Vector2(col * itemWidth, -row * itemHeight);

                // ���⼭ �ʿ��� �����ͳ� UI ��ҵ��� �׸� ä��ϴ�.
                // ���� ���, Text ������Ʈ�� �����ͼ� �ؽ�Ʈ�� �����ϰų�, �̹����� �����ϴ� ���� �۾��� ������ �� �ֽ��ϴ�.
                TextMeshPro textComponent = item.GetComponentInChildren<TextMeshPro>();
                if (textComponent != null)
                    textComponent.text = nicknames[index];
            }
        }
    }
}
