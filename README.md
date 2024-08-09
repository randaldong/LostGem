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

- after taking one damage, apply "Invincibility Frames" (using [Coroutine](https://docs.unity3d.com/Manual/Coroutines.html) vs. :star: ​Async (implemented as [UniTask](https://github.com/Cysharp/UniTask)))
- apply animation for taking damage & death
- get bounced back a little b
- can use `UnityEvent` for a series of events after taking damage



### Attack

- [ ] `attackMode`: {Touch, Hit}



## Combat System

