## To play: 
  ## Download either the output for mouse and keyboard or controller depending on what you want to play with.

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

  
## A7:

For assignment 7, various UI elements were added to expose more of the game to the players, such as a minimap or feedback for the note system. 

Track:
* Extended track to include entire song
* Sync all music sections to track
* Changed out of bounds plane to instead outline the track, helping with the clarity of the track
* Added more elevation variation to track.
* Added a minimap to further assist clarity by improving navigation
* Added a progress bar to show how much of the track remains

Hits:
* Added indication of early, late, and missed notes
* Added combo tracker
* Updated appearance of score and accuracy UI elements

Player:
* Player speed is now shown as a speedometer
* Player hitbox clearly indicated

Sound:
* The background song was edited into different sections, so that longer channels of the track are able to loop the music.
* When the player is not moving, the music stops, and an idle sound plays. The engine idle sound was acquired from freesounds.org.

## A8:

Shaders:
* Hits change the emissions part of the shader if you are in the center of the note. Hold notes also change emission when at the start part.
* Shader of the car changes when power-up is picked up

Forms of writing:
* Opening screen that pops up at the start of the game
* Controls and credits pages
* Ending screen that shows the player their score, accuracy, time, and if they hit par. 

Modifications from alpha to beta:
* Track is now complete
* Driving controls are better
* Driving has acceleration/decceleration
* Notes have different colors
* Added hit sounds for feedback

