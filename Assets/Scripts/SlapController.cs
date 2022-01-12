using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SlapController : MonoBehaviour
{
    [SerializeField] private GameObject enemy, powerBtn, healthBtn, hitIndicator;
    [SerializeField] private HitPower hitPower;
    [SerializeField] private CoinSystem coinSystem;
    [SerializeField] private CameraMove cameraMove;

    private Animator playerAnimator;
    private Animator enemyAnimator;
    public bool playerTurn;
    public bool canclick;

    #region AnimatorStrings
    private static readonly int Preparing = Animator.StringToHash("preparing");
    private static readonly int Knocked = Animator.StringToHash("knocked");
    private static readonly int StandUp = Animator.StringToHash("standUp");
    private static readonly int IsUp = Animator.StringToHash("isUp");
    private static readonly int Lowslap = Animator.StringToHash("lowslap");
    #endregion

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();
        playerTurn = true;
        canclick = true;
        cameraMove.CameraPlayerTurn();

        powerBtn.SetActive(true);
        healthBtn.SetActive(true);
    }


    private void Update()
    {
        if (playerTurn)
        {
            playerAnimator.SetBool(Preparing, true);
            enemyAnimator.SetBool(Preparing, false);

            if (Input.GetMouseButtonDown(0) && !CheckUIClick.IsPointerOverUIObject() && canclick)
            {
                PlayerSlapping();
                hitPower.sequence.Pause();
                hitPower.CheckHitPowerSection();
                powerBtn.SetActive(false);
                healthBtn.SetActive(false);
            }
        }
        else
        {
            enemyAnimator.SetBool(Preparing, true);
            playerAnimator.SetBool(Preparing, false);
        }
    }

    public void PlayerSlapping()
    {
        playerAnimator.SetBool(Preparing, false);

        Hit("player");
        StartCoroutine(DisableIndicator(false, 1));
        canclick = false;
    }

    public IEnumerator EnemySlapping()
    {
        yield return new WaitForSeconds(2);
        enemyAnimator.SetBool(Preparing, false);

        Hit("enemy");
        StartCoroutine(DisableIndicator(true, 1));
    }

    public void Hit(string character)
    {
        if (character == "player")
        {
            playerAnimator.SetTrigger("slap");
        }
        else enemyAnimator.SetTrigger("slap");
    }

    public void GetHit(string character)
    {
        if (character == "player")
        {
            coinSystem.PlayerGetDamage();
            hitPower.sequence.Play();

            if (coinSystem.strongHit)
            {
                StrongSlap(playerAnimator);
            }
            else
            {
                LowSlap(playerAnimator);   
            }
            
            DOVirtual.DelayedCall(0.2f, () =>
            {
                playerTurn = true;
                cameraMove.CameraPlayerTurn();
                coinSystem.strongHit = false;
            });
        }
        else
        {
            StartCoroutine(coinSystem.EnemyGetDamage());

            if (hitPower.CheckHitPowerSection() == 1f)
            {
                StrongSlap(enemyAnimator);
            }
            else
            {
                LowSlap(enemyAnimator);    
            }
            
            DOVirtual.DelayedCall(0.2f, () =>
            {
                playerTurn = false;
                cameraMove.CameraEnemyTurn();
            });
        }
        
        SoundManager.Instance.PlaySound("slap");
        GameManager.Instance.Vibration();
    }

    
    private void StrongSlap(Animator animator)
    {
        animator.SetTrigger(Knocked);
        animator.SetTrigger(StandUp);
        animator.SetTrigger(IsUp);
        SoundManager.Instance.PlaySound("wow");
    }

    private void LowSlap(Animator animator)
    {
        animator.SetTrigger(Lowslap);
    }

    public void StayDown(string character)
    {
        if (character == "player")
        {
            if (coinSystem.playerHealth <= 0)
            {
                playerAnimator.enabled = false;
            }
        }
        else if (character == "enemy")
        {
            if (coinSystem.enemyHealth <= 0)
            {
                enemyAnimator.enabled = false;
            }
        }
    }

    IEnumerator CanClick()
    {
        yield return new WaitForSeconds(0.25f);
        canclick = true;
    }

    IEnumerator DisableIndicator(bool activeState, float time)
    {
        yield return new WaitForSeconds(time);
        hitIndicator.SetActive(activeState);
        hitPower.wasClickedHit = false;
        canclick = false;
    }
}