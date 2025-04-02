# 🎮 C# Console Board Game Framework
This C# console program is designed as a flexible and reusable framework for building two-player board games. It currently includes a playable Connect Four game, with features like **Human vs Human, Human vs Computer, Undo/Redo**, and **Save/Load**. The program architecture allows easy extension to add more game types in the future.

---

# ✅ Key Features
- **🎲 Game Modes**

 - Human vs Human

 - Human vs Computer (computer makes automatic random moves)

- **✅ Move Validation**

 - Ensures human players input valid moves

 - Computer player automatically chooses a valid column

- **💾 Save and Load**

 - Players can save progress anytime and resume later

- **🔄 Undo / Redo**

 - Each player can undo or redo their last move independently

- **🧠 Help and Feedback**

 - Inline help and clear console feedback for valid/invalid inputs

---

# 💡 Design Overview
The codebase is structured around **object-oriented design** principles, promoting clean separation of responsibilities and easy extensibility.

## 🧱 Class Responsibilities
| Class | Responsibility |
|-------|----------------|
| `Game` *(abstract)* | Defines common logic and interface for all game types (e.g., start game, load/save, switch player) |
| `Connect4Game` | Inherits from `Game`; implements all Connect 4–specific logic (win check, auto moves, game loop) |
| `Board` | Manages the visual board, dimensions, rendering, and table updates |
| `Player` | Base class for all players (Human or Computer), including tracking moves, undo/redo functionality |
| `HumanPlayer` / `ComputerPlayer` | Derived from `Player`; provides specific logic for user input or auto decision |
| `Move` | Represents a single move with row and column data |
| `Piece` | Stores and manages the list of symbols used by players (e.g., "O", "X") |

🛠 This architecture allows new games (like Tic-Tac-Toe or SOS) to be added by creating new subclasses of Game with minimal changes to the existing structure.

---
# 🎯Game screenshots
![Board View](./screenshot.png)
![Board View](./screenshot.png)
![Board View](./screenshot.png)
![Board View](./screenshot.png)
![Board View](./screenshot.png)


---
# 🖥 How It Works
**1.Choose a game mode**
  Players select to play against a human or the computer.
  
**2.Make moves in turns**
  Players enter a column number (1–7) to drop their piece.

**3.Computer player auto-moves**
  In Human vs Computer mode, the AI randomly selects a valid column.
  
**4.Game continues**
  Until one player wins (4 pieces connected) or the board is full.

**5.Undo/Redo anytime**
  Each player can revert or redo previous moves using menu options.

**6.Save and resume**
Players can quit and load progress later via JSON serialization.

---

# 🧩 Class Diagram
<img width="920" alt="Screenshot 2023-11-16 at 1 42 35 pm" src="https://github.com/zoewang66/BoardGame/assets/97823545/b462ed12-7cc1-4e3e-9b0b-41d6ec5264e7">
Methods like SaveGameProgress() and LoadGameProgress() are implemented using the System.Text.Json library.The board is initialized inside the Connect4Game constructor.The symbolList dynamically tracks game symbols, which makes the framework flexible for other games.

---

# 📌 How to Run

1.Open the solution in Visual Studio or any C# IDE
2.Build and run the project
3.Follow on-screen instructions in the console

---

# 🌱 Future Improvements
- Add smarter AI logic (not just random moves)
- Add support for other games (e.g., SOS, Tic-Tac-Toe)
- Add GUI version using Windows Forms or WPF
- Add multiplayer over network








