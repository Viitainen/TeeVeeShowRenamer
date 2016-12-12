# TeeVeeShowRenamer
Repo for my TV Show Renaming Software.

This is my first real "application". It's made with C#. If you want to try it, you can download it from https://www.dropbox.com/s/8nebyr3k4c0ytda/TEEVEESHOWRENAMER.zip?dl=0
PROBABLY NOT WORKING WITH WINDOWS 10

The renamer takes video and subtitlefiles, trims the spaces/dots/ etc, tries to find the shows name, episode number and season number. It will delete all words after the season number + episode number so there will be no episode name in the new file name. It does not use any API:s to get these information.
It then merges these to make a new name based on the users pattern/syntax. For example "showsname - Sseason numberEepisode number".
It will rename the files, and if the user has selected to use a library path, it will move the files there. And if there's a folder
which name matches the showsname, it will be moved to this folder.
