Project requires two packages: Player Input and Cinemachine

Using Unity Editor version 2022.3.16f1

## Alpha Software:

Controls mouse and keyboad ("PlayerControls" InputAction Asset)
* Drive with WASD
* Hit with left click
* Steer with mouse

Controls controller ("Xbox" InputAction Asset)
* Drive with Left stick(left and right) and back bumpers (forward and back)
* Hit with left front bumper
* Steer with right stick

When the cylinder is within a red "hit" box, press the hit button to perform a hit

If you get a hit, the top right score will increase. Ratio of pass/fail is in bottom left

Dropping below an accuracy of 50% will slow down the car to 75% speed

Different colored "roads" will play different songs, in preparation for the different loop sections. They will be the same song in actual future tracks. 

**Press Escape key to close game**

## A6 part:

Physics Constructs
* Hit Note. When the player presses the hit key they will get a score multiplied by their combo
* Hold Note. The same as the hit note except the player has to hold the hit key till the end to get max points
* Both Note types use more than one trigger but Hold notes can use many smaller parts to increase the length of the hold

Lights
* Light on Hit note
* Light on Hold Note
* Headlights on the car

Textures
* Notes have a PCB like texture. Provide by Free PBR
* Tracks
* The Car. Provided by TurboSquid

Puzzle And Navigation:
* Players try to get the highest score they can by not missing the notes
* Both note types are a type of trap
* AI racers use Aggrotables, pathfinding a way for AI to hit notes

Mecanim
* Idle animation of car spinning tires
* Car spins when pressing a button
* Turning turns the car

How it comes all together:
* The car will work together with the hit notes to make a rhythm game where the players try to get 
  the highest score they can with the best accuracy. Using PCB textures will help make the game feel
  more "in a computer". AI racers will act like other rouge programs that want all the data to themselves.