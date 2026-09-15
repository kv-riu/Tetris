# Tetris 2D

A classic Tetris game recreation built in Unity, featuring the standard Super Rotation System (SRS), dynamic ghost piece outlines, and responsive controls.

---

## Installation and Quick Start

You can play the standalone Windows version directly without installing Unity:

1. Go to the [Releases](https://github.com/kv-riu/Tetris) section of this repository and download `Tetris.zip`.
2. Extract the contents of `Tetris.zip` to a folder on your PC.
3. Open the extracted folder and run `Tetris.exe` to play.

---

## Controls

| Key | Action | Description |
| :--- | :--- | :--- |
| **Space** / **Enter** | Start / Restart | Start a new session or restart after Game Over |
| **Left / Right** or **A / D** | Move | Move the active piece horizontally |
| **Up** or **W** | Rotate | Rotate the piece 90 degrees clockwise (SRS compliant) |
| **Down** or **S** | Soft Drop | Accelerate the piece downward |
| **Space** | Hard Drop | Instantly drop and lock the piece to the bottom |
| **R** | Reset | Clear the board and reset the game session at any time |

---

## Features

- **Standard Tetrominoes**: Includes all 7 canonical pieces (I, J, L, O, S, T, Z) with distinct color coding.
- **Super Rotation System (SRS)**: Complete wall kick implementation for smooth rotation against borders and obstacles.
- **Ghost Piece**: Real-time silhouette preview indicating the exact landing position of the falling piece.
- **Line Clearing & Scoring**: Automatic detection and clearing of completed lines with score tracking.
