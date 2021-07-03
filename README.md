# CustomerAPI

There is two method , Post and Get

# Requirments :
  1- Create and use sorted internal array
  2- The server also persists

I used a file in order to have a back up and it will update after adding new record to the internal array.
Also in start up and once time, it will check if we already have data or not.
Data will be save the same sorted way we have them in array, so it does not need to re-sord them in read from file

In order to read and write into this file, I defined it on appsettings.json,  "FilePath": "c://tmp//1.json"
Please make sure you have such this folder or just change it

note : if you already notice my name in commit are differents, be aware it coming from git congif in my system and work envirment  
