using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hareket ve Z�plama metodlar�n� bar�nd�r�r.
/// </summary>
public interface IMovement101
{
    public void Move(float speed);  
    public void Jump(float force);
}

public interface IMovement201
{
    public void Crouch();
    public void Run();
}

