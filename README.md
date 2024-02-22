# Unity & Unreal Engine Sample Project

## Game Design Document
2024 - João Milone

### Summary
1. [About Project](#about-project)
2. [Tools](#tools)
3. [Game Mechanics](#game-mechanics)
4. [UI](#ui)
5. [Controls](#controls)

---

## About Project
This is a sample project designed to explore and compare development in both Unity and Unreal Engine. The goal is to achieve similar gameplay mechanics and feel while utilizing the unique features of each engine. 

**Genre:** Action  
**Perspective:** 3D Top-down  
**Main platform:** PC  

---

## Tools
To maintain simplicity and focus on mechanics rather than graphics, the same asset pack, UI images, and animations are used for both projects.

### Assets:
- **Animations:** [Adobe Mixamo](https://www.mixamo.com/)
- **Asset Pack:** [Synty Studios: POLYGON - Prototype Pack](https://syntystore.com/products/polygon-prototype-pack?_pos=1&_psq=proto&_ss=e&_v=1.0)
- **UI Assets:** [Kenny - UI Pack](https://kenney.nl/assets/ui-pack)

### Engines:
Both engines are kept vanilla, without third-party assets, to maximize utilization of their core features.

- **Unity:** Version 2022.3.10f1 - C#
- **Unreal:** Unreal Engine 5.3.2 - Blueprints

---

## Game Mechanics

### Entities
- A base entity class will serve as the foundation for both enemies and the player, featuring components like health, movement, and stats.
- Players choose a character before joining the match.

### Game Mode
- Domination-like mode with bases scattered around the map, protected by enemy NPCs.
- Eliminate all enemies from a base to capture it.
- Players earn points over time when owning bases, aiming to reach a specific target to win.
  
**Win Condition:** Reach domination point amount.  
**Lose Condition:** Player health reaches zero.

### Enemies
- Basic enemies that can take damage and be eliminated with simple animations. AI implementation is deferred.

### Interactables
- Various interactable objects, initially including a healing chest and a light source.
- Interactions can be triggered via both click and hold actions.

### Player Abilities
- Primary ability: Shoot fireballs
- Secondary ability: Cast an AOE spell
- System designed to easily incorporate new abilities.

---

## UI
- Display player health and name, captured bases, and skill cooldowns.

---

## Controls
- **Movement:** WASD
- **Aim:** Mouse
- **Primary Ability:** Mouse 1
- **Secondary Ability:** Mouse 2
- **Interact:** F

---
