# CraftingEngine
Simple terminal crafting game in Visual Basic

---

## Overview:

CraftingEngine is a terminal game based on [Little Alchemy](https://littlealchemy.com/) by Jakob Koziol. Programmed in Visual Basic, CrafitngEngine is open source and can be easily modified to add other recipes or features.
<br>
<br>

## How to Play:

The goal of the crafting engine is to discover all 560 elements by combining two elements together. The game starts with four elements unlocked:
* Air
* Earth
* Fire
* Water

Combine two elements together by typing: **\<Element 1\> + \<Element 2\>**, where \<Element\> is the text name of the element (including spaces). *Be sure to type a plus sign.*
<br>

## Commands

* __\<Element\>__ - Typing the name of an element will list all crafting recipes for that element.

* __List [L]__ - List all unlocked elements. 
  * __[L]__ - (Optional) Typing one or more single letters, numbers, or symbols will search for elements that starts with that character.
          For example, typing __list a b__  will list all elements that starts with A, then all elements that start with B.
          
* __Hint__ - Show a random recipe using the already unlocked elements. _The recipe may have already been unlocked_.

* __Clear__ - Clear all text on the terminal screen.

* __Help__ - Display in-game help screen.

<br>
<br>

## Game Progress

* __Save__ - Save game progress to a text password. _The password is copied onto the clipboard._

* __Load__ - Load the game progress from a text password. _An invalid password will reset game progress._

* __Reset__ - Reset all game progress.

<br>

## Modifying Recipes
---
All crafting recipes come directly from the game Little Alchemy, but can be easily modified by editing the file CraftingRecepies.txt. Recipes have the following format:
* Name,Color:E1+E2;E1+E2;

The element color can be one of the following colors:

| Console Color | \<Color\> | | Console Color | \<Color\> |
| --- |---| --- | --- | --- |
| ConsoleColor.Blue | Blue | | ConsoleColor.DarkBlue | DarkBlue, DKBlue |
| ConsoleColor.Cyan | Cyan | | ConsoleColor.DarkCyan | DarkCyan, DKCyan |
| ConsoleColor.Gray | Gray | | ConsoleColor.DarkGray | DarkGray, DKGray |
| ConsoleColor.Green | Green | | ConsoleColor.DarkGreen | DarkGreen, DKGreen |
| ConsoleColor.Magenta | Magenta | | ConsoleColor.DarkMagenta | DarkMagenta, DKMagenta |
| ConsoleColor.Red | Red | | ConsoleColor.DarkRed | DarkRed, DKRed |
| ConsoleColor.White | White | | ConsoleColor.Black | Black |
| ConsoleColor.Yellow | Yellow | | ConsoleColor.DarkYellow | DarkYellow, DKYellow |
