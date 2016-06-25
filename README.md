# wanderer-project
2d rpg in the works

Player features left and right movement as well as a simple jump. Animations are done through unity's onboard animation system.

Player also has short range and long rage combat mechanics (via lRAttackCol and AttackCol scripts). Attacking works by creating a 
box collider on mouse button click. When an enemy hits the collider, it is destroyed. The collider is nothing more than a trigger,
and the enemy can be easily scripted to die after a couple hits, or die a couple seconds after a hit.

Player has a "radio" feature that plays a selected audio file. It can also be paused and resumed at any time. 

The game is inspired by mysterious real life radio stations that send out coded messages and distorted sequences. They are broadcast on
shortwave radio waves and are thought to be relics of the cold war. The game utilizes these "number stations" as a game mechanic. As the 
player progresses through levels, they hear these number stations and must use the messages in sent to unlock terminals, which let the player continue on.
