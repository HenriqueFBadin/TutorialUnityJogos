using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class BattingManager : MonoBehaviour
{
    public enum BattingStates { Idle, Ready, Swing, Hit, Miss, Final }

    private BattingStates currState;
    public GameObject ball;
    public GameObject messageTextPrefab;
    private float swingTime;
    public float minX, maxX, minY, maxY;
    private Vector2 mousePosition;
    private float readyStartTime;
    private BallMovement ballMovement;
    private static int _strikes;
    private static int _balls;
    private static int _strikeState;
    private bool isBallInRange;

    private float validStartTime;
    private float validEndTime;

    public AudioClip hitSound;
    public AudioClip catchSound;
    private AudioSource audioSource;
    public TMP_Text strikeUI;
    public TMP_Text ballUI;
    public TMP_Text timerUI;
    private static float _totalTime;

    void Start()
    {
        currState = BattingStates.Idle;
        Cursor.visible = true;
        ballMovement = ball.GetComponent<BallMovement>();
        audioSource = GetComponent<AudioSource>();
        strikeUI.GetComponent<TextMeshProUGUI>().text = _strikes.ToString();
        ballUI.GetComponent<TextMeshProUGUI>().text = _balls.ToString();
    }

    void Update()
    {
        _totalTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_totalTime / 60f);
        int seconds = Mathf.FloorToInt(_totalTime % 60f);
        timerUI.text = $"Time: {minutes:00}:{seconds:00}";
        if (currState == BattingStates.Idle)
        {
            Cursor.visible = false;
            HandleCursorMovement();

            if (ball != null && ballMovement.GetIsPitching())
            {
                currState = BattingStates.Ready;
                if (_totalTime > 30f)
                {
                    validEndTime = ballMovement.GetTimeToImpact() + 0.75f;
                    validStartTime = validEndTime - 0.75f;
                }
                else if (_totalTime > 45f)
                {
                    validEndTime = ballMovement.GetTimeToImpact() + 1f;
                    validStartTime = validEndTime - 1f;
                }
                else if (_totalTime > 60f)
                {
                    validEndTime = ballMovement.GetTimeToImpact() + 1.25f;
                    validStartTime = validEndTime - 1.25f;
                }
                else
                {
                    validEndTime = ballMovement.GetTimeToImpact() + 0.5f;
                    validStartTime = validEndTime - 0.5f;
                }
                readyStartTime = Time.time;
            }
        }

        if (currState == BattingStates.Ready)
        {
            HandleCursorMovement();

            float now = Time.time;

            if (Input.GetMouseButtonDown(0))
            {
                CreateBallShadow(ball.transform.position);
                Cursor.lockState = CursorLockMode.Confined;

                swingTime = Time.time;
                float t = swingTime - readyStartTime;

                if (t >= validStartTime && t <= validEndTime)
                    currState = BattingStates.Swing;
                else
                {
                    currState = BattingStates.Miss;
                    _strikeState = 1;
                }
            }
            else if ((now - readyStartTime) >= validEndTime)
            {
                currState = BattingStates.Miss;
            }
        }

        if (currState == BattingStates.Swing)
        {
            if (isBallInRange)
                currState = BattingStates.Hit;
            else
                currState = BattingStates.Miss;
        }

        if (currState == BattingStates.Hit)
        {
            StartCoroutine(HandleHitRoutine());
            currState = BattingStates.Final;
        }

        if (currState == BattingStates.Miss)
        {
            if (_strikeState == 1)
                StartCoroutine(HandleStrikeRoutine());
            else if (_strikeState == 2)
                StartCoroutine(HandleBallRoutine());

            currState = BattingStates.Final;
        }
    }

    IEnumerator HandleHitRoutine()
    {
        audioSource.PlayOneShot(hitSound);
        ShowMessage("Hit!");
        yield return new WaitForSeconds(1f);

        ResetCounters();
        Cursor.visible = true;
        SceneManager.LoadScene("End");
    }

    IEnumerator HandleStrikeRoutine()
    {
        _strikes++;
        audioSource.PlayOneShot(catchSound);
        if (_strikes < 3)
        {
            ShowMessage($"Strike {_strikes}");
            yield return new WaitForSeconds(1f);
            strikeUI.GetComponent<TextMeshProUGUI>().text = _strikes.ToString();
            ReloadBattingScene();
        }
        else
        {
            ShowMessage("Strike 3! Batter' Out!");
            yield return new WaitForSeconds(1.5f);
            strikeUI.GetComponent<TextMeshProUGUI>().text = _strikes.ToString();
            ResetCounters();
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator HandleBallRoutine()
    {
        _balls++;
        audioSource.PlayOneShot(catchSound);
        if (_balls < 4)
        {
            ShowMessage($"Ball {_balls}");
            yield return new WaitForSeconds(1f);
            ballUI.GetComponent<TextMeshProUGUI>().text = _balls.ToString();
            ReloadBattingScene();
        }
        else
        {
            ShowMessage("Ball 4! Walk!");
            yield return new WaitForSeconds(1.5f);
            ballUI.GetComponent<TextMeshProUGUI>().text = _balls.ToString();
            ResetCounters();
            SceneManager.LoadScene("End");
        }
    }

    void ReloadBattingScene()
    {
        SceneManager.LoadScene("Batting");
    }

    void ResetCounters()
    {
        _strikes = 0;
        _balls = 0;
    }

    void ShowMessage(string msg)
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiMsg = Instantiate(messageTextPrefab, canvas.transform);
        uiMsg.GetComponent<TextMeshProUGUI>().text = msg;
        Destroy(uiMsg, 2f);
    }

    void HandleCursorMovement()
    {
        if (Camera.main != null)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(mousePosition.y, minY, maxY);
        transform.position = new Vector2(clampedX, clampedY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
            isBallInRange = true;
    }

    public static void SetStrikeStatus(bool isStrike)
    {
        _strikeState = isStrike ? 1 : 2;
    }

    public BattingStates GetCurrState() => currState;

    void CreateBallShadow(Vector3 ballPosition)
    {
        GameObject shadow = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shadow.transform.position = ballPosition;
        shadow.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        Renderer r = shadow.GetComponent<Renderer>();
        Color c = r.material.color;
        c.a = 0.3f;
        r.material.color = c;

        Destroy(shadow, 5f);
    }

    public static float GetTotalTime()
    {
        return _totalTime;
    }

    public static void ResetTime()
    {
        _totalTime = 0f;
    }
}
