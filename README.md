# LostGem

2D pixel sidescroller made with Unity



## Art Design

### Main Character

[Generic Character Asset v 0.2](https://brullov.itch.io/generic-char-asset)

15 Animations:

- [ ] Idle;

- [ ] Combo sword attack;
- [ ] Critical hit (attack);
- [ ] Running;
- [ ] Walking;
- [ ] Jumping;
- [ ] Falling;
- [ ] Sliding;
- [ ] Wall sliding;
- [ ] Taking damage;
- [ ] Ladder climbing;
- [ ] Crouch;
- [ ] Shield defense;
- [ ] Spell cast;
- [ ] Death.



### Monsters

#### Boar



### Environment

[Legacy Fantasy](https://anokolisa.itch.io/sidescroller-pixelart-sprites-asset-pack-forest-16x16)



## Animator System

### Idle

### Walk

### Run



### Jump

### Crouch



### Take Damage

Could take damage from any state

Add a TAKE-DAMAGE FLASH effect in a new layer



## Stat System

### Health

- [ ] `maxHealth`
- [ ] `curHealth`
- [ ] `invincibleTime`

Take damage:

- after taking one damage, apply "Invincibility Frames" (using [Coroutine](https://docs.unity3d.com/Manual/Coroutines.html) vs. ​Async (implemented as [UniTask](https://github.com/Cysharp/UniTask)))
- apply animation for taking damage (may trigger lots of times in a short time, better to use a trigger instead of bool)
- get bounced back a little bit & disable movement (freeze for a while)
- can use `UnityEvent` for a series of events after taking damage



Death:

- after `curHealth == 0`, paly death animation (use bool), disable gameplay input control



### Attack

- [ ] `attackMode`: {Touch, Hit}

- click mouse left button to perform attack
- 3-hit combo attack

## Combat System

