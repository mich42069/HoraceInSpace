# Specification of the final project for relevant C# courses
(When modifying this document, please maintain the layout and structure and follow the inline instructions.)

## C# Courses selection
(Change `[ ]` to `[x]` for the courses you plan to use this final project for.)

- [x] NPRG035 (Programming in C# language | Programování v jazyce C#)
- [x] NPRG038 (Advanced C# Programming | Pokročilé programování v jazyce C#)
- [ ] NPRG057 (Advanced .NET Programming II | Pokročilé programování pro .NET II)
- [ ] NPRG064 (Programming user interfaces in .NET | Programování uživatelských rozhraní v .NET)

## Specification

### Horace in space (Asteroids) (with Enemies (Asteroids and UFOs))

---
A physical continuous simulation of asteroids and UFOs and Horace's spaceship in fullscreen, with local score keeping system and stars in the background.

---

The game will have the already mentioned enemies, a background separate from the playing area with flickering stars, horace, main menu where we can see our best score.

I want the game to feel actually playable but still a bit challanging. The ideal usecase for this game is to play it on lectures that aren't fun.

The GUI and all enemies etc. will be done using the Monogame library. 

The scoreboard will be done using a simple JSON serializer, so it can be stored as an object and be easily loaded and updated as needed.

Functionality of is this game fun will be tested by everyone that is unfortunately in my vicinity while I need testers. Other then that there will be an ability to pause or play at a slower speed (an argument), so we can see if the hitreg works as intended. There will also be a setting (an argument) that allows the player to see hitboxes of all enemies and even himself.

As for the advanced features, there will be extension methods used for the physical simulation library, Interface method overloada for the updating and drawing of entities (im not sure if that is advanced or normal c#) and I wanted to have the update of the background stars flickering be done in parallel on another thread with everything else, since I am pretty sure it won't be very efficient.

