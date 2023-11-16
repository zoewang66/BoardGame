# Design summary
The C# Program is designed to make it easy to create different two-player board games that is extensible and reusable.
The following features have been implemented:

Different game modes can be chosen:
 - Human vs. Human
 - Human vs. Computer

Human players’ moves would be validation if it is following the rules
1. A valid move of computer player would be randomly selected by system
2. Games can be saved and loaded for any point and resume by player
3. Games allow players to undo and redo moves
4. Games provided inline help information about the gameplay, valid move message, and error messages for invalid moves.

Overall design to be easy to modify, extend, and maintain. Different classes would handle a specific part of the game.

a. The Game class is an abstract class that defines the common features and functionalities of all games which is easier to new game types by simply inheriting from the Game class    and implementing the method and logic for the game. New Connect4 can be added by creating new classes from the Game class.
b. The Board class is responsible for managing the game board, including its dimensions, and rendering the board to the console.
c. The Piece class is responsible for initizalize and keep different symbols for different games that can be dynamically track different games’ players’ symbols move list.
d. The Player class and its derived classes handle player interactions and moves. This allows for different situation for different game, it can handle Human player and Computer      player.
e. The Move class represents the fundamental elements of the games. When players (Human Player, Computer Player) started to make a new move, it would create a new move object to      save the current move to the player move list. In addition, when players implement redo or undo function, the recent move object which saved in player move list and would be       get by players to achieve the function.
f. The game also has features for saving and loading the game progress, undo/redo moves status, which offer players flexibility in the game.

Players interact with the game through console inputs, feedback, instructions, and error messages in the console would be provided directly.

# Class diagram
Methods such as SaveGameProgress() and LoadGameProgress() were implemented from JSON library in Game class to make it easier to modify, extend, and maintain.
We initialize and generate board inside of Connect4Game constructor.
We created a string type list called symbolist to save different symbols for different games which is flexible to adapt each game.

<img width="920" alt="Screenshot 2023-11-16 at 1 42 35 pm" src="https://github.com/zoewang66/BoardGame/assets/97823545/b462ed12-7cc1-4e3e-9b0b-41d6ec5264e7">

