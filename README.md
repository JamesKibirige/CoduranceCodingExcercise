# CoduranceCodingExcercise
A console-based social networking application (similar to Twitter )


## Getting Started

Two Options are available for running the application:
1. Clone the repo and download the source files to compile and run the application via Visual Studio.
2. Downloading some precompiled and published executable files:

   [CoduranceCodingExcercise/PublishedExecutables/SocialMessengerPublish.zip](../master/PublishedExecutables/SocialMessengerPublish.zip)
   Extract and unpack the zip file and look for the following file: "run.cmd" Run by opening the file.

### Prerequisites

Make sure that the Unit and Integration Test projects have the following nuget packages:
+ [Moq](https://www.nuget.org/packages/moq/)
+ [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/)

### Installing

*A step by step series of examples that tell you how to get a development env running*

## Running the tests

*Explain how to run the automated tests for this system*

## Deployment

*Add additional notes about how to deploy this on a live system*

## User Instructions

Interact with the application via a Console based command line interface, submit commands as messages via the Console. There are 5 commands available to users.

### Commands

#### Posting

>(user name) -> (message)

Post messages to a Users timeline, a new user will be created when you post a message for the first time
```
James -> Hello my name is James Kibirige
```

#### Reading

>(user name)

Read the messages that a user has published
```
James
```

#### Following

>(user name) follows (another user)

Subscribe to another users timeline
```
James follows Sophie
```

#### Wall

>(user name) wall

View an aggregated set of all messages for a Users subscriptions as well the the Users own published messages
```
James wall
```

#### Exit

>£exit£

Exit from the application
```
£exit£
```

## Authors

* **James Kibirige** - *Initial work* - [CoduranceCodingExcercise](https://github.com/JamesKibirige/CoduranceCodingExcercise)
