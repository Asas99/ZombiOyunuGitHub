using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hareket ve Zıplama metodlarını barındırır.
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

