using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponRigs : MonoBehaviour
{
    public Transform lh_ik_target;
    public Transform rh_ik_target;

    public Transform lh_pistol;
    public Transform rh_pistol;

    public Transform lh_refile;
    public Transform rh_refile;

    private Transform curLhTrans;
    private Transform curRhTrans;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lh_ik_target.position = curLhTrans.position;
        rh_ik_target.position = curRhTrans.position;

        lh_ik_target.rotation = curLhTrans.rotation;
        rh_ik_target.rotation = curRhTrans.rotation;
    }

    public void WeaponPistol()
    {
        curLhTrans = lh_pistol;
        curRhTrans = rh_pistol;
    }

    public void WeaponRifle()
    {
        curLhTrans = lh_refile;
        curRhTrans = rh_refile;
    }
}
