# AI Elective | Unity & MLAgents

> Welcome to the Unity & MLAgents workshop. In this workshop we will set up and play around with MLAgents in Unity. This tutorial is based on Unity's own tutorial at https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Getting-Started.md

## What is MLAgents?

> MLAgents is a framework you can use on top of Unity that allows you to teach your so-called Agent how to behave in your Unity environment. Behind the scenes, it uses Reinforcement Learning to reward desired behaviour (for example: scoring a goal in a soccer game), while punishing undesired behaviour (for example, scoring an own goal in a soccer game, or letting the other team score). 

## Getting Started 

### Setting up the environment

In order to get started, several things are needed:

 - Unity (version 2021.3 or later)
 - The `ml-agents` Unity package
 - `anaconda` (visit ==anaconda.com/download== and download the latest version)

Anaconda is a powerful environment / package management tool that is widely used in order to manage working with various (mostly python) packages. We will be using it to install pytorch (a python machine learning library) and other dependencies we need to run MLAgents in Unity.  

Now, after installing anaconda, run the program `anaconda powershell prompt`. The resulting screen should display something similar to this: `(base) PS C:\Users\Steven Warmelink>`. In this terminal, run the following commands in succession:

1. `conda create -n 'AI_elective' python==3.8.13`
2. `conda activate AI_elective`
3. `python -m pip install mlagents==0.30.0`
4. `pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html`
5. `pip3 install protobuf==3.20`
6. `mlagents-learn --help`

At this point, you should see the mlagents help text. This should be enough to get started in Unity!

### Creating a Unity Scene

To make things easier, you can download the initial scene setup from (this) github:

> `git clone https://github.com/StevenWarmelink/CMGT_AI_elective.git`

