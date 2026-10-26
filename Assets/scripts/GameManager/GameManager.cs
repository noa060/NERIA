// ============================================
// 1. GameManager.cs - 메인 게임 매니저 (Singleton)
// ============================================

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    //[SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    private bool isPaused = false;
    private float totalPlaytime = 0f;

    // Events
    public delegate void GameEventHandler();
    public event GameEventHandler OnEpisode1Complete;
    public event GameEventHandler OnEpisode2Start;
    public event GameEventHandler OnEpisode2Complete;
    public event GameEventHandler OnGameOver;

    private void Awake()
    {
        // Singleton 패턴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 첫 페이드인
        //StartCoroutine(FadeIn(fadeDuration));
    }

    private void Update()
    {
        if (!isPaused)
        {
            totalPlaytime += Time.deltaTime;
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneName));
    }

    private  IEnumerator ChangeSceneCoroutine(string sceneName)
    {
        //yield return StartCoroutine(FadeOut(fadeDuration));
        SceneManager.LoadScene(sceneName);
        //yield return StartCoroutine(FadeIn(fadeDuration));
        yield return null;
    }

    //private IEnumerator FadeOut(float duration)
    //{
        //float elapsed = 0f;
        //while (elapsed < duration)
        //{
            //elapsed += Time.deltaTime;
            //fadePanel.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            //yield return null;
        //}
        //fadePanel.alpha = 1f;
    //}

    //private IEnumerator FadeIn(float duration)
    //{
        //float elapsed = 0f;
        //while (elapsed < duration)
        //{
            //elapsed += Time.deltaTime;
            //fadePanel.alpha = Mathf.Lerp(1, 0, elapsed / duration);
            //yield return null;
        //}
        //fadePanel.alpha = 0f;
    //}

    public void PauseGame() => isPaused = true;
    public void ResumeGame() => isPaused = false;
    public float GetPlaytime() => totalPlaytime;
}
