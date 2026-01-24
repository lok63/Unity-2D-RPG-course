using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void DisableMovementAndJump() => _player.EnableMovementAndJump(false);
    private void EnableMovementAndJump() => _player.EnableMovementAndJump(true);
    
}
