using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private enum PitchType
    {
        FastBall,
        // CurveBall,
        // Slider,
        ChangeUp
    }
    
    public BattingManager battingManager;
    private PitchType pitchType;
    public float speed = 10f;
    public float spin = 100f;

    public Transform strikeZone;  
    private Vector3 targetPosition;
    
    private bool hasReachedStrikeZone;
    private float timeToImpact;
    private bool isPitching;  // Flag que indica se a bola está se movendo

    private float idleStartTime;

    private float changeUpSpeed;
    private float curveBallSpeed;
    private float minSpeed;
    
    void Start()
    {
        SetRandomTargetPosition(); 

        idleStartTime = -1;
        minSpeed = speed * 0.6f;
    }

    void Update()
    {
        if (battingManager.GetCurrState() == BattingManager.BattingStates.Idle)
        {
            if (idleStartTime < 0)
            {
                idleStartTime = Time.time;
            }
            else
            {
                float deltaTime = Time.time - idleStartTime;
                if (deltaTime >= 21.0f)
                {
                    int sorteio = Random.Range(0, 2);
                    if (sorteio == 0)
                    {
                        pitchType = PitchType.FastBall;
                    }
                    else
                    {
                        pitchType = PitchType.ChangeUp;
                    }
                    StartPitch(pitchType);
                }
            }
        }
        if (isPitching)
        {
            MoveBall();
            ApplySpin();
        }
    }

    private void StartPitch(PitchType pitch)
    {
        pitchType = pitch;
        isPitching = true;
        if (pitchType == PitchType.ChangeUp)
        {
            changeUpSpeed = speed;  // Reinicia a velocidade do ChangeUp para a inicial
        }
    }

    void MoveBall()
    {
        float step = speed * Time.deltaTime;
        
        if (pitchType == PitchType.FastBall)
        {
            // FastBall: Trajetória reta e rápida
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }

        else if (pitchType == PitchType.ChangeUp)
        {
            // ChangeUp: Começa rápido como FastBall e desacelera
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (changeUpSpeed > minSpeed)
            {
                changeUpSpeed -= 0.1f;  // Diminui a velocidade aos poucos até atingir o valor mínimo
            }
        }

        timeToImpact = Vector3.Distance(transform.position, targetPosition) / speed;

        if (!hasReachedStrikeZone && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            hasReachedStrikeZone = true;
            idleStartTime = -1;
            isPitching = false;

            if (IsInStrikeZone())
            {
                BattingManager.SetStrikeStatus(true);
            }
            else
            {
                BattingManager.SetStrikeStatus(false);
            }
        }
    }

    void ApplySpin()
    {
        //Função que aplica rotação. Estava dando erro então tirei
    }

    bool IsInStrikeZone()
    {
        Collider2D zoneCollider = strikeZone.GetComponent<Collider2D>();
        if (zoneCollider != null)
        {
            return zoneCollider.bounds.Contains(transform.position);
        }
    
        return false;
    }

    public float GetTimeToImpact()
    {
        return timeToImpact; 
    }

    public bool GetIsPitching()
    {
        return isPitching;
    }
    
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(strikeZone.position.x - 2.0f, strikeZone.position.x + 2.0f);
        float randomY = Random.Range(strikeZone.position.y - 2.0f, strikeZone.position.y + 2.0f);

        targetPosition = new Vector3(randomX, randomY, strikeZone.position.z);
    }

}
