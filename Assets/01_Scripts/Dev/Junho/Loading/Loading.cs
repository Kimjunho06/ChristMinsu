using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    static int nextScene = 0;

    public float loadingDelay = 0f;
    [Range(0f, 1f)]
    public float delayPoint = 0f;

    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Image tipImage;
    [SerializeField] private TextMeshProUGUI tipNumText;
    [SerializeField] private TextMeshProUGUI tipText;

    [SerializeField] private List<Image> tipImageList;
    [SerializeField] private List<string> tipTextList;

    private int currentTip = 1;

    private void Start()
    {
        currentTip = Random.Range(1, tipTextList.Count);
        StartCoroutine(LoadSceneProcess());
    }

    private void Update()
    {
        ChangeTip();
    }

    public static void LoadScene(int sceneNum)
    {
        nextScene = sceneNum;
        SceneManager.LoadScene("Loading");
    }

    public void OnNextTip()
    {
        if (currentTip >= tipTextList.Count)
        {
            currentTip = 1;
        }
        else
        {
            currentTip++;
        }
    }

    private void ChangeTip()
    {
        tipNumText.text = $"#{currentTip} Game Tip";
        tipText.text = $"{tipTextList[currentTip-1]}";
        tipImage.sprite = tipImageList[currentTip-1].sprite;
    }

    IEnumerator LoadSceneProcess()
    {
        // LoadScene = ���� => �� �ҷ����� �� �ƹ��͵� �� �� ����.
        // LoadSceneAsync = �񵿱� => �� �ҷ����� �� �۾��� ������.
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 

        // ���� �񵿱�� �ҷ��� �� �� �ε��� ������ �ҷ��� ������ �̵������� ����� ���
        // false�� ��� ���� 90�۱��� �ε��ϰ� �Ѿ�� �ʰ� ��ٸ�. true�� �ٲپ� �������� �ε���.
        // �����ֱ� ������ �־��. (Tip ����, �� �ε��� �ִ°�?��� �ǹ� ����)
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < delayPoint)
            {
                loadingSlider.value = op.progress;
            }
            else // fake loading
            {
                timer += Time.unscaledDeltaTime;
                loadingSlider.value = Mathf.Lerp(0.9f, 1f, timer);
                if (loadingSlider.value >= 1f)
                {
                    yield return new WaitForSeconds(loadingDelay); //�ε� �ð� ����
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}