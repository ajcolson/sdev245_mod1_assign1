# SDEV245 Module 1 Assignment 1
> [!CAUTION]
> There is minimal string sanitizing on console inputs for this assignment, so check your inputs carefully.

This is a minimal, CLI based list tracking program. It allows for multiple users to create and manage separate, password protected lists.

For the purposes of the assignment, two accounts are created at startup:
- User
  - A generic user that has their own list.
  - Credentials:
    - Id: user
    - Password: user
- Admin
  - Same as a generic user, but also can manage user accounts.
  - Credentials:
    - Id: admin
    - Password: admin

# Commands
## General Commands
| Command | Description |
| --- | --- |
| `login` | Prompts for user login and authenticates. Starts a session if successful. |
| `logout` | Ends a session, if it exists. |
| `clear` | Clears the screen. |
| `help` | Shows a list of all commands.<br>_The output changes depending on if a user is signed in and if the user is an admin._ |
| `exit` | Closes the program. |

## User List Commands
> These commands require a user to be signed in.

| Command | Description |
| --- | --- |
| `list` | Shows the current user's list.|
| `add` | Add a new item to the current user's list.|
| `remove` | Remove an item from the current user's list by item number.|
| `removeall` | Remove all items for the current user's list.|
| `passwd` | Change the password of the current user. |

## Admin Commands
> These commands require the admin account to be signed in.

| Command | Description |
| --- | --- |
| `listuser` | Shows a list of all users. |
| `adduser` | Add a new user. _Requires the admin user to be signed in._|
| `removeuser` | Remove an existing user.<br>___Note: The admin account cannot be removed.___ |
| `passwduser` | Change a password for a user. |
