/* 
Things that I have to do:
NPC rooms: make one scene room that instantiates different npcs/items depending on the room entered.
    - if the character triggers the "door" (doors 1,2,3, etc.) they will enter the npc scene, which goes through a list? of NPCs 
    that correspond to the number and the items that correspond to the number.
        - NPCs need different text codes, sprites and sounds.
    - you walk over the item to buy it (lose the rupees, gain item) and everything in shop disappears
    but when you come back to the scene the same items are reinstantiated again.

Player stats: internal coding, not UI for now - how many you have at any time, static variable that can be used in the NPC shops
    - health (other codes need an if/then state - if your health < 6 (3 hearts, each is 1/2) then you can't shoot your sword)
    - rupees (for shop - if your rupee count is < a number, walking over it doesn't do anything; if your rupee is > a number, you buy it)
    - bombs (other codes need an if/then state - if your bomb count is < 1, you cannot use them, if it is > 0 you can use them.)

Pickups: trigger collider with an item that an enemy drops (don't have to code enemy drop, just trigger event)
increases the static variable of player stats for each thing picked up, or removed when used. Destroy the object in the scene after 
you trigger it. 
    - Pickups need different sprites and sounds
        - hearts (if you collide, ++;)
        - rupees (if you collide with one type, +1, if you collide with the other, +5)
        - bombs (if you collide, ++;)

Will need to directly collaborate with:
- player code (for bombs, movements, picking up items, health based attack)
- map code (for triggers for the npc room)
- enemy code (to drop the items on death that you pick up for player stats)
- UI code (to display what you have on you at any given moment)

To check out
- all location 1 NPC locations
- all location 1 NPC loot
- all location 1 NPC text
*/