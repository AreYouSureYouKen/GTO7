using UnityEngine;
using System.Collections.Generic;

public class FSMState : MonoBehaviour {

    protected Dictionary<Transition, FSMStateID> map;
    protected FSMStateID stateID;
    public FSMStateID ID { get { return stateID; } }
    protected Vector3 destPos;
    protected Transform[] waypoints;

    [SerializeField]
    protected float curRotSpeed;
    [SerializeField]
    protected float curSpeed;

    public virtual void Setwaypoints(Transform[] wp)
    {
        waypoints = wp;
        map = map = new Dictionary<Transition, FSMStateID>();
    }

    public void AddTransition(Transition transition, FSMStateID id)
    {
        // Check if anyone of the args is invallid
        if (transition == Transition.None || id == FSMStateID.None)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        //Since this is a Deterministc FSM,
        //Check if the current transition was already inside the map
        if (map.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        map.Add(transition, id);
        Debug.Log("Added : " + transition + " with ID : " + id);
    }

    public void DeleteTransition(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition passed was not on this State´s List");
    }

    public FSMStateID GetOutputState(Transition trans)
    {
        // Check for NullTransition
        if (trans == Transition.None)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return FSMStateID.None;
        }

        // Check if the map has this transition
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }

        Debug.LogError("FSMState ERROR: " + trans + " Transition passed to the State was not on the list");
        return FSMStateID.None;
    }

    public virtual void Reason(Transform player, Transform npc) { }

    public virtual void Act(Transform player, Transform npc) { }

    public void FindNextPoint()
    {
        //Debug.Log("Finding next point");
        int rndIndex = Random.Range(0, waypoints.Length);
        Vector3 rndPosition = Vector3.zero;
        destPos = waypoints[rndIndex].position + rndPosition;
    }

    protected bool IsInCurrentRange(Transform trans, Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - trans.position.x);
        float zPos = Mathf.Abs(pos.z - trans.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }
}
