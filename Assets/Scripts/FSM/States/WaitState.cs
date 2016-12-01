using UnityEngine;
using System.Collections;

public class WaitState : FSMState {

    [SerializeField]
    private float _visionRange;
    [SerializeField]
    private Transition _inVisionRange = Transition.SawPlayer;


    public override void Setwaypoints(Transform[] wp)
    {
        base.Setwaypoints(wp);
        stateID = FSMStateID.Waiting;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if(Vector3.Distance(npc.position,player.position) <= _visionRange)
        {
            Debug.Log("Switch to Chase State");
            npc.GetComponent<BasicEnemyFSM>().SetTransition(_inVisionRange);
        }
    }
}
