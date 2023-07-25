using RPG.Attributes;
using System.Collections;
using RPG.Control;
using UnityEngine;
using RPG.Movement;
using RPG.SceneManagement;

namespace RPG.Combat
{
    public class AIBoss : MonoBehaviour
    {
        [SerializeField] float iFramesDuration;
        [SerializeField] int numberOfFlashes;
        [SerializeField] GameObject bossBase;
        [SerializeField] GameObject bossRed;
        [SerializeField] GameObject healEffect;
        [SerializeField] WeaponConfig flippedWeapon;
        [SerializeField] WeaponConfig normalWeapon;

        Health health;
        Fighter fighter;
        GameObject player;
        Transform playerBackPoint;
        Animator animator;
        Mover mover;

        bool firstAttack = true;
        bool secondAttack = true;
        bool thirdAttack = true;
        bool fourthAttack = true;
        bool fifthAttack = true;
        bool bossDefeated = false;
        bool finalAttack = false;
        bool countdownStarted = false;
        bool rageMode = false;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerBackPoint = player.GetComponent<PlayerController>().GetBackPoint();
            health.SetInvulnerable(true);
            bossRed.SetActive(false);
            bossBase.SetActive(true);
        }

        private void Update()
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().IsDead())
            {
                Reseted();            
            }
            if (health.GetHealthPoints() <= 14999 && !countdownStarted)
            {
                StartCoroutine(Countdown());
            }
            else if (health.GetPercentage() <= 80 && firstAttack)
            {
                StartCoroutine(FirstAttack());
            }
            else if (health.GetPercentage() <= 60 && secondAttack)
            {
                StartCoroutine(SecondAttack());
            }
            else if (health.GetPercentage() <= 40 && thirdAttack)
            {
                StartCoroutine(ThirdAttack());
            }
            else if (health.GetPercentage() <= 20 && fourthAttack)
            {
                StartCoroutine(FourthAttack());
            }
            else if (health.GetPercentage() <= 10 && fifthAttack)
            {
                StartCoroutine(FifthAttack());
            }
            else if (health.GetHealthPoints() <= 1 && !bossDefeated)
            {
                StartCoroutine(BossDefeated());
            }
            else if (health.GetHealthPoints() <= 1 && finalAttack && fighter.enabled)
            {
                StartCoroutine(FinalAttack());
            }
        }

        private void Reseted()
        {
            StopAllCoroutines();      
            bossRed.SetActive(false);
            bossBase.SetActive(true);
            fighter.SetTimeBtwAttack(1f);
            animator.speed = 1f;
            health.SetInvulnerable(false);
            mover.SetMaxSpeed(4f);
            firstAttack = true;
            secondAttack = true;
            thirdAttack = true;
            fourthAttack = true;
            fifthAttack = true;
            bossDefeated = false;
            finalAttack = false;
            countdownStarted = false;
            rageMode = false;
            animator.Rebind();
        }

        private IEnumerator FirstAttack()
        {
            firstAttack = false;
            animator.SetTrigger("startLooping");
            animator.SetBool("isLooping", true);
            mover.ChangeMaxSpeed(1.7f);
            yield return new WaitForSeconds(10);
            mover.ChangeMaxSpeed(-1.7f);
            animator.SetBool("isLooping", false);
        }

        private IEnumerator SecondAttack()
        {
            secondAttack = false;
            animator.SetBool("isBlocking", true);
            GetComponent<AIController>().SetIsBlocking(true);
            fighter.Cancel();
            for (int i = 0; i < 5; i++)
            {
                health.Heal(700);
                GameObject effect = Instantiate(healEffect, transform);
                yield return new WaitForSeconds(1);
                Destroy(effect, 5);
            }
            fighter.Attack(player);
            GetComponent<AIController>().SetIsBlocking(false);
            animator.SetBool("isBlocking", false);
        }

        private IEnumerator ThirdAttack()
        {
            thirdAttack = false;
            animator.SetTrigger("startLooping");
            animator.SetBool("isLooping", true);
            mover.ChangeMaxSpeed(1.7f);
            yield return new WaitForSeconds(10);
            mover.ChangeMaxSpeed(-1.7f);
            animator.SetBool("isLooping", false);
        }

        private IEnumerator FourthAttack()
        {
            fourthAttack = false;
            animator.SetBool("isBlocking", true);
            GetComponent<AIController>().SetIsBlocking(true);
            fighter.Cancel();
            for (int i = 0; i < 5; i++)
            {
                health.Heal(700);
                GameObject effect = Instantiate(healEffect, transform);
                yield return new WaitForSeconds(1);
                Destroy(effect, 5);
            }
            fighter.Attack(player);
            GetComponent<AIController>().SetIsBlocking(false);
            animator.SetBool("isBlocking", false);
        }

        private IEnumerator FifthAttack()
        {
            fifthAttack = false;
            health.SetInvulnerable(true);

            bossBase.SetActive(false);
            bossRed.SetActive(true);
            fighter.SetTimeBtwAttack(0.25f);
            animator.speed = 4f;
            for (int i = 0; i < numberOfFlashes; i++)
            {
                transform.position = playerBackPoint.position;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

                transform.position = playerBackPoint.position;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }
            bossRed.SetActive(false);
            bossBase.SetActive(true);
            fighter.SetTimeBtwAttack(1f);
            animator.speed = 1f;
            health.SetInvulnerable(false);
        }

        private IEnumerator BossDefeated()
        {
            bossDefeated = true;
            if (rageMode)
            {
                bossRed.SetActive(false);
                bossBase.SetActive(true);
                fighter.SetTimeBtwAttack(1f);
                animator.speed = 1f;
            }
            health.Heal(1);
            animator.Rebind();
            fighter.EquipWeapon(flippedWeapon);
            animator.SetBool("isSiting", true);
            animator.SetTrigger("sit");
            fighter.Cancel();

            GetComponent<AIController>().SetIsBlocking(true);
            health.SetInvulnerable(true);

            yield return new WaitForSeconds(5);
            finalAttack = true;
        }

        private IEnumerator FinalAttack()
        {
            finalAttack = false;
            fighter.EquipWeapon(normalWeapon);
            animator.SetBool("isSiting", false);
            animator.Rebind();
            GetComponent<AIController>().SetIsBlocking(false);
            health.SetInvulnerable(false);
            yield return new WaitForSeconds(6);
            FindObjectOfType<SavingWrapper>().LoadCreditsMenu();
        }

        private IEnumerator Countdown()
        {
            countdownStarted = true;
            for (int i = 0; i < 12; i++)
            {
                if (health.IsDead()) yield break;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().IsDead())
                {                   
                    Reseted();
                    yield break;
                }
                yield return new WaitForSeconds(10f);
            }
            
            if (bossDefeated) yield break;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().IsDead()) yield break;
            rageMode = true;
            bossBase.SetActive(false);
            bossRed.SetActive(true);
            fighter.SetTimeBtwAttack(0.5f);
            animator.speed = 2f;
            mover.SetMaxSpeed(8f);
        }
    }
}