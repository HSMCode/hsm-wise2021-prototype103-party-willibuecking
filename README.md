# DJ-Dungeon

[PLAY](docs/index.html)

This project was imported using the **Unity 2020.3.x** empty base project.

This is a one-button Game developed in the course "One-Button-Games" at Hochschule Mainz in Winter 2021/22 supervised by Prof. Linda Kruse.
In DJ-Dungeon you take on the role of a DJ on the way to a party. Before you get there, you have to assemble your music. Two song have been split into pieces and scattered aroung the rooms. While passing the doors of rooms, you have to listen which track is inside the adjacent room. When entering a room (by pressing Space), you add the track to your song and move on. When your track is assembled, enter one of the party rooms and check if the audience likes your music.

## Summary

  - [Getting Started](#getting-started)
  - [Code Structure](#code-structure)
  - [Authors](#authors)
  - [License](#license)
  - [Acknowledgments](#acknowledgments)

## Getting Started

To get play the game, simply click the "PLAY"-Link at the top of this page. Alternatively open it in the Unity-Editor version 2020.3.x or later.

## Project Structure and Mechanics

### Movement and World-generation
The Base of the Game is the **PlayerMover**. The Camera is always centered on this object. It contains the **PlayerBody**. The **PlayerMover** constantly rotates around itself and thus moves the **PlayerBody** which is offset towards the room-walls.
The Rooms are split into two categories: **MusicRooms** and **PartyRooms**. Additionally there is the **StartRoom**.
When the **PlayerMover** enters a collider infront of the door of the room it is in, a light lights up and the **PlayerMover** becomes movable. If Space is pressed during this time, the **PlayerMover** will finish its rotation until the next full quarter-circle is reached and then move to the middle of the adjacent room.
When the **PlayerMover** enters the Collider of a **MusicRoom**, all the rooms around the previous room are destroyed, except for the old one itself and the one that was entered. Also, the newly entered room instantiates three new rooms around it on the empty sides: 2 **MusicRooms** and one **PartyRoom**, where the **PartyRoom** alternates between being a Black-Metal-Party and a Techno-Party. If the **PlayerMover** enters a **PartyRoom**, the game ends, no new rooms are built and the score is calculated.

### Music
The **AudioTracks** are all in the scene at the beginning of the Game. For each Track of each Song (10 in total) there is an **AudioTrack**-Object somewhere. It is too far away from the **PlayerBody** (which contains the audio-listener) to be audible. All the Tracks Play on Awake and loop to ensure synchronicity. This way, they will match no matter at what point in time each track is called or collected. The **SceneMaster**-Object contains a List of all AudioTracks, split into **ATracks** (for Black Metal) and **TTracks** (for Techno). When a **MusicRoom** is built, it takes a Track-Name from the List in the **SceneMaster**, starting with the highest item-number and sequentially moving down as the Player moves through the rooms. Each Track will only be called once. The **AudioTrack** is then placed in the middle of the **MusicRoom**. If the **PlayerMover** enters the room and thus enters a collider attached to the **AudioTrack**, the Object is "collected" (the transform.parent is set to be the **PlayerMover** and the Track-Volume is turned down significantly to enable the player to still hear Tracks in other rooms). If a Track is collected, it´s points (a value set individually for each Track) are added to a counter contained in **SceneMaster**. There are two counters: Techno-points and Black-Metal-points.

### Game-End and score
If the game ends (if the player enters a **PartyRoom**), the points of the wrong music are substracted from the points of the right music (i.e. entering a Black-Metal-PartyRoom means the Techno-points are substracted from the Black-Metal-points). The result is the players score and displayed on screen. The maximum score is 30 points (for exclusively collecting all Tracks of a single genre and bringing them into the right **PartyRoom**) and the minimum is -30 points (for exclusively collecting all Tracks of a single genre and bringing them into the wrong **PartyRoom**).

## Authors

  - **Billie Thompson** - *Provided README Template* -
    [PurpleBooth](https://github.com/PurpleBooth)
  - **Willi Bücking** - *Made the Game* -


See also the list of
[contributors](https://github.com/PurpleBooth/a-good-readme-template/contributors)
who participated in this project.

## License

This project is licensed under the [GNU gpl-2.0](LICENSE.md)