using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hareket ve Zýplama metodlarýný barýndýrýr.
/// </summary>
public interface IMovement101
{
    public void Move(float speed);  
    public void Jump(float force);
}

public interface IMovement201
{
    public void Crouch(float speed, float multiplier);
    public void Run(float speed, float multiplier);
}

