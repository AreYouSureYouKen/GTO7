using UnityEngine;
using System.Collections;

public class AttackState : FSMState {
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private float VisionRange;

    [SerializeField]
    private Transition _outOfAttackRange =  Transition.SawPlayer;
    [SerializeField]
    private Transition _outOfVisionRange = Transition.LostPlayer;

    public override void Setwaypoints(Transform[] wp)
    {
        base.Setwaypoints(wp);
        stateID = FSMStateID.Attacking;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float distance = Vector3.Distance(npc.position, player.position);
        if (distance >= AttackRange)
        {
            npc.GetComponent<BasicEnemyFSM>().SetTransition(_outOfAttackRange);
        }
        if (distance >= VisionRange)
        {
            npc.GetComponent<BasicEnemyFSM>().SetTransition(_outOfVisionRange);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        // do attacking here?
        npc.GetComponent<BasicEnemyFSM>().PubMoveFSM(npc.transform.position, curSpeed, curRotSpeed);
        npc.GetComponent<BasicEnemyFSM>().TryAttack();
    }
}
