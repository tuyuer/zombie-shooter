using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponRigs : MonoBehaviour
{
    public Transform lh_ik;
    public Transform rh_ik;

    public Vector3 pistol_lh = new Vector3(0.08f, 1.47f, 0.365f);
    public Vector3 pistol_rh = new Vector3(0.2f, 1.5f, 0.3f);

    public Vector3 rifle_lh = new Vector3(0.08f, 1.47f, 0.365f);
    public Vector3 rifle_rh = new Vector3(0.2f, 1.5f, 0.3f);

    private Vector3 lh_pos;
    private Vector3 rh_pos;

    // Start is called before the first frame update
    void Start()
    {
        WeaponPistol();
    }

    // Update is called once per frame
    void Update()
    {
        lh_ik.localPosition = lh_pos;
        rh_ik.localPosition = rh_pos;
    }

    public void WeaponPistol()
    {
        lh_pos = pistol_lh;
        rh_pos = pistol_rh;
    }

    public void WeaponRifle()
    {
        lh_pos = rifle_lh;
        rh_pos = rifle_rh;
    }
}
