

After 1 minute, pee system turns on

Pee value runs until certain value

Once its full at certain value, separate countdown from 30 happens

if player has not peed yet after countdown, bool will turn on (PlayerHasPeedThemselves)

if player has peed, bool will turn on (PlayerHasPeed)

if "PlayerHasPeed", player loses 30 seconds on the main timer
if "PlayerHasPeedThemselves", player loses 60 seconds on the main timer

After any of those bools turn on, a cooldown would commence

after cool down, pee value runs and its on loop.



#### Exclusions 

- When in shelter, the time value moves up more
- In hypo mode, the time value goes down
- In the last few seconds, pee system turns off











