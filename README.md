# Kumio: Gamified Digital Heritage Route for HoloLens 2

This research focuses on the value of gamification in raising digital heritage awareness. We offer two gamified museum experiences in mixed reality: one using a handheld device (Samsung Galaxy Tab S8 Ultra tablet) and the other using a wearable device (HoloLens 2). Both versions incorporate game elements to enhance the instructional experience. MRTK 2 Toolkit for HoloLens2 is used for development of this project.

## Key Features:

### Interactive Quest Game: 
Follow a precisely planned route within Yedikule Fortress in Istanbul, designed to maximize historical involvement and learning. Collect hidden coins throughout the fortress to uncover its secrets and solve a final puzzle.
### Educational Quizzes: 
Quizzes are strategically placed along the route to test players on historical knowledge about the sites they visit. These educational quizzes aim to enhance learning without affecting the game's outcome.
### Immersive Diegetic Elements: 
Utilize diegetic elements and historical decorations to add to the immersive experience. These visual and contextual elements help players recall the historical narrative and context of the locations they visit.

## Devices Supported:

- Android Augmented Reality (AR) Tablet - Samsung Galaxy Tab S8 Ultra
- Mixed Reality (MR) Head-Mounted Display - Microsoft HoloLens 2

## Unity version: 2020.3.45f1

## Download Instructions:

### HoloLens 2:
- Ensure your Microsoft HoloLens 2 is updated to the latest firmware.
- Download the Kumio-H project as a zip file from this repository.
- Extract the zip file to a desired location on your computer.
- Open the extracted project in Unity.
- Go to Window > Package Manager.
- Click on the "+" button and select "Add package from git URL..."
- Enter the MRTK 2 GitHub URL: https://github.com/microsoft/MixedRealityToolkit-Unity.git?path=/Assets/MRTK.
- Click "Add" to import the MRTK 2 toolkit into your project.
- In Unity, go to File > Build Settings.
- Select Universal Windows Platform as the target platform.
- Set the Target Device to HoloLens and the Architecture to x86.
- Click Build and select a folder to save the build files.
- Once the build process is complete, open the generated solution file (.sln) in Microsoft Visual Studio.
- In Visual Studio, select Release and x86 as the build configuration.
- Ensure your HoloLens 2 is connected and recognized by Visual Studio.
- Click on Deploy to install the app on your HoloLens 2.
- Launch the app on your HoloLens 2 and explore the digital heritage route.

## Project Support
This is a research project by project Istanbul Technical University supported by TÜBİTAK, project number 122K268.

## Contact us: kumio.itu0@gmail.com





