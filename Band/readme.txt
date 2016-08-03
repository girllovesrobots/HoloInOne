//Be sure to add the Microsoft.Band NuGet package

using BandAPI;

//Start the connection to the band using:

BandAPI band = new BandAPI(); //Creates instance of BandAPI class
band.StartBandAPI(); //calls the start Band method

//BandAPI.getAcceleration() //gets the current acceleration
//BandAPI.vibrateBand(VibrateBand.Win) //sends a vibration sequence to the band