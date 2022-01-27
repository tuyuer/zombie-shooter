using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public class Character : MonoBehaviour
{
    public Weapon weapon = null;

    private ActorBlood characterBlood;
    public ActorBlood Blood
    {
        get { return characterBlood; }
    }

    private Blackboard blackBoard;

    void Awake()
    {
        characterBlood = GetComponent<ActorBlood>();
        blackBoard = GetComponent<Blackboard>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive())
            return;

        //if (GlobalDef.ENABLE_STICKJOY)
        //{
        //    if (GameWorld.Instance.aimstick.IsHolding)
        //    {
        //        weapon.Shoot();
        //    }
        //}
        //else
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        weapon.Shoot();
        //    }
        //}
    }

    public void OnDeath()
    {
        blackBoard.animator.SetTrigger(AnimatorParameter.Die);
        int nLayerIndex = blackBoard.animator.GetLayerIndex("WeaponLayer");
        blackBoard.animator.SetLayerWeight(nLayerIndex, 0);
        MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_PLAYER_DEATH);
    }

    public void OnDamage() 
    {
        MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_PLAYER_DAMAGE);
    }

    public bool IsAlive()
    {
        return characterBlood.GetFillAmount() > 0;
    }
}
