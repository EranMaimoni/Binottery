# Binottery
The “Binottery” game

Game description:
The lottery board/card structure contains 27 numbers and looks like (kind of a Bingo board)

• The first column contains numbers from 0 to 9, the second column numbers from 10 to 19, the third, 20 to 29 and so on up until the last column, which contains numbers from 80 to 89.

• The application populates the board with random numbers that are shown to the player; In addition, the application chooses 6 numbers randomly from the board (and doesn’t show the player the chosen cells)

• The player has 5 tries to discover the chosen numbers

• If the player’s guess is correct, a “*+*” is printed near the chosen number

• If the player’s guess is not correct, a “*-*” is printed near the chosen number.

• Every success the player gets 1 point credit. A credit balance is printed below the board every time the board is printed. If the player succeeds to discover 5 numbers, his credit is doubled

• After each try, the updated board is re-printed

• The board should be printed where each column is aligned to left (see examples below)

Input Commands summary:
1. new

Starts a new game, if there is no saved game on the disk

2. continue

Continues a saved game on the disk

3. show

Shows the active board, or generate new board if there is no active board. This is the actual game start

4. end

Ends the current game

5. exit

Exits the application

6. 0-89 numeric

The numbers that are entered by the player

Game flow

• After the 5th try, the game is ended automatically, and the credit is printed to the player.

• If the player enters the command “exit”, the game exits immediately, but if it was in a middle of
a game (meaning there were already 1-4 tries by the player), the current state of the game is
written to a file on the disk.

• Once the application starts it checks if there is a game on a file it can continues from. If there is
an “open game” on the file, it asks the player if he wishes to continue the game or to start a new
one. You can assume only one game can be saved on the disk.

• If the player enters the command “end”, the current game is ended. The board is cleaned. The
player can call “end” at any time. A credit balance of the ended game is printed.

• If the player enters the command “show”, the current board is shown. If there is no active board
(for example after the player “end” command, or right after the application is started), the
application will compute the board randomly and the board will be printed.


