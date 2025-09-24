using UnityEngine;
/// <summary>
/// A generic Rigidbody based way to apply a set amount of force to a rigidbody2D
/// </summary>
public static class Knockback
{
    /// <summary>
    /// Does knockback to a given rigidbody from the origin to the given target
    /// </summary>
    /// <param name="knockbackForce">The amount of force that has to be applied to the target</param>
    /// <param name="originPos">The origin position of the vector</param>
    /// <param name="targetPos">The endpoint position of the vector</param>
    /// <param name="targetRB">The affected rigidbody component</param>
    public static void DoKnockback(float knockbackForce, Transform originPos, Transform targetPos, Rigidbody2D targetRB)
    {
        //Get the direction of force from the origin to the target
        Vector3 forceVector = (targetPos.position - originPos.position).normalized;

        //Add the force in the direction calculated above
        targetRB.AddForce(forceVector * knockbackForce, ForceMode2D.Impulse);
    }
}
