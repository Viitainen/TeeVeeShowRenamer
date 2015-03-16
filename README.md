# TeeVeeShowRenamer
Repo for my TV Show Renaming Software

This is my first real application. It's made with Visual Studio C#.

The renamer takes video and subtitlefiles, trims the spaces/dots/ etc, tries to find the shows name, episode number and season number.
It then merges these to make a new name based on the users pattern/syntax. For example "showsname - Sseason numberEepisode number".
It will rename the files, and if the user has selected to use a library path, it will move the files there. And if there's a folder
which name matches the showsname, it will be moved to this folder.
