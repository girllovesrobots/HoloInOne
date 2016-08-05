Go to Assets > Double Click Level1, open up in Unity
Go to File > Build Settings > (reference the picture) and save in a folder (named VSBuild)
Go to VSBuild, double click Holo In one. Since Unity does not support compilation of Windows specific APIs, we worked around by doing if real app....

We want these to be on when we upload the app to the HoloLens, so go to Assembly-CSharp, Properties (reference in picture), and add REAL_APP; to the build settings

---
**Requires Microsoft Band to play

This mini-golf game was created as to explore the integration of the Microsoft Band as an controller for the HoloLens. 

To play the game, first pair your Microsoft Band to the HoloLens. To pair the band, first turn on pairing on the Band. On the Start menu in HoloLens, go to Settings > Bluetooth. Your Band should appear as one of the connectable Bluetooth devices. Follow the prompts on the Band and HoloLens screen. If successful, the Band will appear as "Connected" on the HoloLens' list of Bluetooth devices.

The first time you open the application, a prompt should appear onscreen, asking you if you'd like to allow the application to access the Band. Select "Yes" and you should be good to go!
To play the game, simply swing your arm while wearing the Band to hit the ball. If you hit the ball, you should feel a vibration coming from the Band. The projection of the ball (essentially the direction that the ball will travel in) is shown through a red line coming from the golf ball. If the ball is stuck in a corner or you would like to reset the ball, simply focus your Gaze on the ball and perform the Pinch gesture.
