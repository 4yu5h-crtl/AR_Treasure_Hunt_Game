# 🕹️ AR Treasure Hunt Game  
An **Augmented Reality Treasure Hunt** experience built with **Unity** and **Vuforia**, where players scan real-world images to uncover clues, collect letters, and reveal a hidden treasure chest!  

---

## 🌟 Overview  
This project blends the excitement of **AR exploration** with **interactive storytelling**. Players embark on a clue-based journey — each scanned image reveals a hidden letter and hint for the next stage. When all clues are found, a grand **treasure chest** appears along with an exciting celebration sound and congratulatory message!  

---

## 🎮 Gameplay Flow  
1. **Scan the first image** → the letter *M* appears automatically and is counted in the progress.  
2. **Receive the next hint** → follow it to the next image.  
3. **Scan subsequent targets** to uncover all hidden letters.  
4. On the final clue, the **treasure chest appears** with a celebration message and sound effect!  

---

## 🧬 Features  
- ✨ **Vuforia-powered AR tracking** for real-world interaction.  
- 🎯 **Automatic clue detection** — no clicks required!  
- 🔔 **Sound effects** for letter collection and final treasure reveal.  
- 💬 **Dynamic hints** and progress updates via TextMeshPro UI.  
- 🪙 **Animated treasure chest** with sound when the hunt is completed.  

---

## 🧠 Tech Stack  
| Component | Description |
|------------|-------------|
| **Unity (2021+ recommended)** | Core game engine |
| **Vuforia Engine** | Image tracking & AR rendering |
| **C# Scripts** | Game logic & interaction handling |
| **TextMeshPro** | UI for hints and progress |
| **Audio Manager** | Plays sound effects for clues and treasure |

---

## 📁 Project Structure  
```
Assets/
├── Scripts/
│   ├── ClueManager.cs
│   ├── TargetHandler.cs
│   
├── Prefabs/
│   ├── Gold Chest
│   
├── Audio/
│   ├── ding.wav
│   └── treasure.wav
└── Scenes/
    └── HuntScene.unity
```

---

## 🦯 Setup Instructions  
1. Open the project in **Unity**.  
2. Ensure **Vuforia Engine** is installed and activated.  
3. Assign your **Image Targets** (M, I, T) under the **ARCamera** object.  
4. Link:  
   - `ClueManager` → all letters, UI texts, and audio sources.  
   - `TargetHandler` → respective image targets and `ClueManager`.  
5. Build the project for **Android** or **iOS**.  
6. Install the APK → Launch → Begin your treasure hunt!   

---

## 📸 Screenshots  
*(Add gameplay screenshots or AR preview images here)*  

---

## 📜 License  
This project is released under the **MIT License**.  
You’re free to use, modify, and share — just credit the author.

