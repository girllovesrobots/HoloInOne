# HoloInOne
Microsoft //OneWeek Hackathon


To Run the Accelerometer UWP application:
- Download the Samples folder.
- Open the project solution in Microsoft Visual Studio
- Make sure you have the Microsoft.Band NuGet Package installed. To do this, go to Tools > NuGet Package Manager > Package Manager Console
- In the Console, change Default Project to **Accelerometer\Accelerometer.Universal**, then type: PM> **Install-Package Microsoft.Band**
- Go to Build > Build Solution.
- Click on Accelerometer.Universal in the Solution Explorer and go to Build > Deploy Accelerometer.Universal
- To run the project, make sure the Band is paired to your dev machine:
  * On your Band, flip to the Settings tile, click the Bluetooth icon and switch to Pairing. Your Band is now in pairing mode. 
  * On your Windows 10 PC, go to Settings and open the Bluetooth settings page. Note the status of your Band in the list of Bluetooth devices. Band names usually start with your name and a code, unless you changed it to something else (in this case, I have “Kevin’s Band ec:5a”). If the status says “Connected,” you’re good to go; otherwise, tap Pair and follow the prompts. [Source] (https://msdn.microsoft.com/en-us/magazine/mt573717.aspx)
