using UnityEngine;
using System.Collections;

public class ChaseState : FSMState {
    [SerializeField]
    private float AttackRange;
    [SerializeField]
    private float VisionRange;

    [SerializeField]
    private Transition _inAttackRange = Transition.ReachPlayer;
    [SerializeField]
    private Transition _outOfVisionRange = Transition.LostPlayer;

    public override void Setwaypoints(Transform[] wp)
    {
        base.Setwaypoints(wp);
        stateID = FSMStateID.Chasing;
    }

    public override void Reason(Transform player, Transform npc)
    {
        float distance = Vector3.Distance(npc.position, player.position);
        if(distance <= AttackRange)
        {
            npc.GetComponent<BasicEnemyFSM>().SetTransition(_inAttackRange);
        }
        if (distance >= VisionRange)
        {
            npc.GetComponent<BasicEnemyFSM>().SetTransition(_outOfVisionRange);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        npc.GetComponent<BasicEnemyFSM>().PubMoveFSM(player.transform.position, curSpeed, curRotSpeed);
    }


}
