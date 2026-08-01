# Horace In Space - User Documentation

## How to Open the Game

To start **Horace In Space**, run the game executable: "HoraceInSpace.exe"

The game will open in the main menu.

The main menu contains three options:

- **Play** - starts a new game.
- **Scoreboard** - displays the highest saved scores.
- **Exit** - closes the game.

Use **arrow keys** to navigate the menu and press **ENTER** to select an option.

---

# How to Play

You control **Horace**, a spaceship trapped in space and surrounded by hostile objects.

The goal is to survive as long as possible, destroy enemies, and achieve the highest score.

## Controls

| Input | Action |
|---|---|
| Mouse | Aim Horace's ship |
| Left Mouse Button | Shoot |
| W | Move forward |
| S | Move backward |
| A | Move left |
| D | Move right |
| ESC | End the current game |

Horace moves using spaceship thrusters. Because there is very low friction in space, movement continues after accelerating, and the ship must be controlled carefully.

---

# Gameplay

## Lives

Horace starts with **3 lives**.

When hit by an enemy or an enemy projectile:

- A life is lost.
- Horace respawns in the center of the map.
- Horace becomes temporarily invincible for a short time.

When all lives are lost, the game ends and the score can be saved.

---

## Enemies

During the game, waves of enemies appear.

The number and difficulty of enemies increase as time passes.

### Asteroids

Asteroids are the main enemies.

- Shooting an asteroid destroys it and gives score.
- Larger asteroids split into smaller asteroids when destroyed.
- Smaller asteroids must also be destroyed to completely clear the wave.

Be careful, as any asteroids can damage Horace on hit.

### UFOs

UFOs are dangerous enemies that can actively attack Horace.

- UFOs shoot bullets back at the player.
- UFO projectiles can damage Horace just like collisions with enemies.

Avoid their fire while trying to destroy them.

---

# Scoring

Points are awarded for destroying enemies.

To get a high score:

- Destroy as many enemies as possible.
- Survive longer to reach harder waves.
- Destroy all asteroid fragments created after splitting.

Your final score is shown when the game ends.

---

# Saving Your Score

After losing all lives, the game will ask for your name.

Controls:

| Input | Action |
|---|---|
| Keyboard typing | Enter your name |
| BACKSPACE | Delete characters |
| ENTER | Save score |
| ESC | Skip saving |

If no name is entered, the score is saved as: "Anonymous"

---

# Scoreboard

The scoreboard can be accessed from the main menu.

It displays the highest saved scores, sorted from highest to lowest.

Scores are stored locally in: "scoreboard.json"

The file is created automatically in the game's working directory after the first saved score.

Deleting this file will reset the scoreboard.

---

# Optional Startup Arguments

The game supports additional startup arguments that can be added when launching the executable.

| Argument | Alternative | Description | Example |
|---|---|---|---|
| `--hitboxes` | `-h` | Displays entity collision hitboxes during gameplay. | `HoraceInSpace.exe -h` |
| `--speed=<number>` | `-s <number>` | Changes the game speed multiplier. Values greater than 1 make the game faster, values between 0 and 1 slow it down. | `HoraceInSpace.exe -s 2` |
---

# Tips

- Use the mouse to aim.
- Avoid staying near large asteroids, as on hit they split into multiple smaller threats.
- UFOs are especially dangerous because they can shoot back, so keep moving while UFOs are on screen.
- Higher waves provide more enemies and more opportunities to increase your score.