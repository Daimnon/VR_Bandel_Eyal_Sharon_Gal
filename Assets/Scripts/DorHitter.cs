using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorHitter : MonoBehaviour
{
    private const string _dorTag = "Dor";

    private void OnCollisionEnter(Collision collision)
    {
        EnableDorsRagdoll(collision);
    }
    private void OnTriggerEnter(Collider other)
    {
        EnableDorsRagdoll(other);
    }

    private void EnableDorsRagdoll(Collision collision)
    {
        if (collision.transform.root.CompareTag(_dorTag))
        {
            BasicDor basicDor = collision.collider.GetComponentInParent<BasicDor>();
            DorMover dorMover = collision.collider.GetComponentInParent<DorMover>();

            if (dorMover.IsLerping)
                dorMover.ShouldStopLerping = true;

            basicDor.StartAllRagdolls();
        }
    }
    private void EnableDorsRagdoll(Collider other)
    {
        if (other.transform.root.CompareTag(_dorTag))
        {
            BasicDor basicDor = other.GetComponentInParent<BasicDor>();
            DorMover dorMover = other.GetComponentInParent<DorMover>();

            if (dorMover.IsLerping)
                dorMover.ShouldStopLerping = true;

            basicDor.StartAllRagdolls();
        }
    }
}
