using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    GameObject player;
    GameObject boss;
    public Vector3 p1;
    public Vector3 p2;
    float rollRotation;
    public float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        rollRotation = animator.transform.position.x - player.transform.position.x;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (rollRotation > 0)
        {
            boss.transform.position = Vector2.MoveTowards(
                boss.transform.position,
                p1,
                speed * Time.deltaTime
            );
            if (Mathf.Approximately(Vector3.Distance(boss.transform.position, p1), 0f))
            {
                animator.ResetTrigger("Attack0");
                animator.ResetTrigger("HitEnemy");
                animator.SetBool("Idle", true);
                boss.GetComponent<Boss>().isWait = true;
            }
        }
        if (rollRotation < 0)
        {
            boss.transform.position = Vector2.MoveTowards(
                boss.transform.position,
                p2,
                speed * Time.deltaTime
            );
            if (Mathf.Approximately(Vector3.Distance(boss.transform.position, p2), 0f))
            {
                animator.ResetTrigger("Attack0");
                animator.ResetTrigger("HitEnemy");
                animator.SetBool("Idle", true);
                boss.GetComponent<Boss>().isWait = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
