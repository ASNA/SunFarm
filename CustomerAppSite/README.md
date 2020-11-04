# SunFarm Customer App Site
This Sample Project depends on the following ASNA-RND github project

[ASNA Expo Support](https://github.com/asnarnd/QSys)

## After Cloning this Repository, please:

1. Clone the QSys/Expo source.
2. Update ..\SunFarm\CustomerAppSite\libman.json file, with your local references for:

```
[7]  "library": "QSys/src/Expo/ExpoTags/src/WebsiteResources/css"
[16] "library": "QSys/src/Expo/ExpoTags/src/WebsiteResources/js"
[76] "library": "QSys/src/Expo/ExpoTags/src/WebsiteResources/js/bterm/"

3. If the references are succesful, you will see the wwwroot file structure updated, such that:

```
wwwroot       

        .  
        .  
        .  
            lib
                 asna  
                    css  
                    js  

                 



