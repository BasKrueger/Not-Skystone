using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkyStone : MonoBehaviour
{
    [field:SerializeField]
    public SkyStoneSpike northSpikes { get; private set; }
    [field:SerializeField]
    public SkyStoneSpike eastSpikes { get; private set; }
    [field:SerializeField]
    public SkyStoneSpike southSpikes { get; private set; }
    [field:SerializeField]
    public SkyStoneSpike westSpikes { get; private set; }

    [HideInInspector]
    public int handIndex = -1;
    [HideInInspector]
    public int ownerIndex { private get; set; } = -1;
    [HideInInspector]
    public Vector3 targetPosition = new Vector3();

    private RandomMovement movement;
    private Animator anim;

    private const float speed = 3;
    private const float rotationForce = 10;

    private void Awake()
    {
        movement = GetComponentInChildren<RandomMovement>();
        anim = GetComponent<Animator>();
    }

    public void SetSpikes(int north, int east, int south, int west)
    {
        northSpikes.SetValue(north);
        eastSpikes.SetValue(east);
        southSpikes.SetValue(south);
        westSpikes.SetValue(west);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector3 direction = targetPosition - transform.position;
            transform.position += direction * speed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, rotationForce * Time.deltaTime);
    }

    public void GrabUpdate(Vector3 mouseDelta)
    {
        transform.Rotate(new Vector3(-mouseDelta.y, 0, mouseDelta.x) * 0.25f);
    }

    public void Animate(bool state)
    {
        movement.enabled = state;
        anim.enabled = state;
    }

    public bool CanGrab(int playerIndex)
    {
        return ownerIndex == playerIndex;
    }

    public void Highlight(bool state)
    {
        anim.SetBool("highlighted", state);
    }

    public void PlayEntryAnimation()
    {
        anim.Play("Entry");
    }

    public void PlaySwitchAnimation()
    {
        anim.Play("Switch");
    }

    #region Animation Events
    public void OnEntryAnimationFinish()
    {
        movement.enabled = false;
    }

    public void IncreaseIntensity()
    {
        movement.range = 0.05f;
        movement.durationLength = 0.25f;
        movement.enabled = true;
    }

    public void ResetIntensity()
    {
        movement.Reset();
    }
    #endregion

}
