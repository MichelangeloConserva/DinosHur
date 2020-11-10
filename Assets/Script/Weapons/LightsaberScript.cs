using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberScript : MonoBehaviour, IWeapon
{
    public float cooldown = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Use();
        }
    }

    public void Use()
    {

        LevelController.Instance.PlaySound(SoundType.LightsaberSwing, transform.position);
    }

}
