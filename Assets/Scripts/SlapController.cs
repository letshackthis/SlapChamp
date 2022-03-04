using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlapController : MonoBehaviour
{
    [SerializeField] private GameObject enemy, powerBtn, healthBtn, hitIndicator;
    [SerializeField] private GamePlayPlace gamePlayPlace;
    [SerializeField] private HitPower hitPower;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private ParticleSystem playerParticles;
    [SerializeField] private ParticleSystem enemyParticles;
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
    private static readonly int mediumslap = Animator.StringToHash("mediumSlap");
    private static readonly int mediumslap2 = Animator.StringToHash("mediumSlap2");

    #endregion

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        enemyAnimator = enemy.GetComponent<Animator>();
        playerTurn = true;
        canclick = true;

        powerBtn.SetActive(true);
        healthBtn.SetActive(true);
    }


    private void Update()
    {
        if (playerTurn)
        {
            playerAnimator.SetBool(Preparing, true);
            enemyAnimator.SetBool(Preparing, false);

            if (Input.GetMouseButtonDown(0) && !CheckUIClick.IsPointerOverUIObject() &&
                !EventSystem.current.IsPointerOverGameObject() && canclick)
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
        enemyParticles.Clear();
        enemyParticles.Pause();
        enemyParticles.Stop();
        yield return new WaitForSeconds(2);
        enemyAnimator.SetBool(Preparing, false);

        Hit("enemy");
        StartCoroutine(DisableIndicator(true, 1));
    }

    public void Hit(string character)
    {
        int randomValueSlap = Random.Range(0, 7);
            if (character == "player")
            {
                gameManager.SetPlayerPower();
                float percentage = (float) gameManager.playerHit / gameManager.playerPower;
                Debug.Log(gameManager.playerHit);
                Debug.Log(gameManager.playerPower);
                if (percentage >= 0.9f)
                {
      
                    playerAnimator.SetTrigger("Slap" + randomValueSlap);
                }
                else
                {
                    playerAnimator.SetTrigger("Slap" + 8);
                }
            }
            else
            {
                gameManager.EnemyAttackPower();
                float percentage = (float) gameManager.dmgPwr / gameManager.enemyPower;
                if (percentage >= 0.9f)
                {
                    enemyAnimator.SetTrigger("Slap" + randomValueSlap);
                }
                else
                {
                    enemyAnimator.SetTrigger("Slap" + 8);
                }
            }
    }

    public void GetHit(string character)
    {
        if (character == "player")
        {
            gameManager.PlayerGetDamage();
            hitPower.sequence.Play();

            SetAnimationPlayerSlap(playerAnimator, gameManager.dmgPwr, gameManager.enemyPower, playerParticles);

            DOVirtual.DelayedCall(0.2f, () =>
            {
                playerTurn = true;
                cameraMove.CameraPlayerTurn();
                gameManager.strongHit = false;
            });
        }
        else
        {
            StartCoroutine(gameManager.EnemyGetDamage());

            SetAnimationPlayerSlap(enemyAnimator, gameManager.playerHit, gameManager.playerPower, enemyParticles);

            DOVirtual.DelayedCall(0.2f, () =>
            {
                playerTurn = false;
                cameraMove.CameraEnemyTurn();
            });
        }

        SoundManager.Instance.PlaySound("slap");
    }

    private void SetAnimationPlayerSlap(Animator animator, int hitCurrentPower, int maxPower,
        ParticleSystem particleSystem)
    {
        float percentage = (float) hitCurrentPower / maxPower;

        if (percentage <= 0.3f)
        {
            Debug.Log("Low");
            animator.SetTrigger(Lowslap);
            gamePlayPlace.Bad();
        }
        else if (percentage > 0.3f && percentage <= 0.9f)
        {
            Debug.Log("Medium");

            bool randomValue = Random.value > 0.3f;

            if (randomValue)
            {
                animator.SetTrigger(mediumslap);
                particleSystem.Play();
            }
            else
            {
                animator.SetTrigger(mediumslap2);
            }

            if (Random.value > 0.3f)
            {
                gamePlayPlace.Good();
            }
            else
            {
                gamePlayPlace.Bad();
            }
        }
        else
        {
            Debug.Log("High");

            animator.SetTrigger(Knocked);
            animator.SetTrigger(StandUp);
            animator.SetTrigger(IsUp);

            gamePlayPlace.Good();
        }
    }

    public void StayDown(string character)
    {
        if (character == "player")
        {
            if (gameManager.playerHealth <= 0)
            {
                playerAnimator.enabled = false;
            }
        }
        else if (character == "enemy")
        {
            if (gameManager.enemyHealth <= 0)
            {
                enemyAnimator.enabled = false;
            }
        }
    }

    IEnumerator CanClick()
    {
        yield return new WaitForSeconds(0.25f);
        canclick = true;
        playerParticles.Stop();
    }

    IEnumerator DisableIndicator(bool activeState, float time)
    {
        yield return new WaitForSeconds(time);
        hitIndicator.SetActive(activeState);
        hitPower.wasClickedHit = false;
        canclick = false;
    }
}