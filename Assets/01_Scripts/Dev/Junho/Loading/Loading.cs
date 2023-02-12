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
        // LoadScene = 동기 => 씬 불러오는 중 아무것도 할 수 없음.
        // LoadSceneAsync = 비동기 => 씬 불러오는 중 작업이 가능함.
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 

        // 씬을 비동기로 불러올 때 씬 로딩이 끝나면 불러온 씬으로 이동할지를 물어보는 경우
        // false인 경우 씬을 90퍼까지 로드하고 넘어가지 않고 기다림. true로 바꾸어 나머지를 로딩함.
        // 보여주기 식으로 넣어둠. (Tip 보기, 왜 로딩을 넣는가?라는 의문 제거)
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
                    yield return new WaitForSeconds(loadingDelay); //로딩 시간 지연
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }
}