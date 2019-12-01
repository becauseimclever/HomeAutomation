# HomeAutomation

Home Automation is a central server platform for automating your home.

[![BCH compliance](https://bettercodehub.com/edge/badge/Fortinbra/HomeAutomation?branch=master)](https://bettercodehub.com/)
[![Build Status](https://becauseimclever.visualstudio.com/HomeAutomation/_apis/build/status/Fortinbra.HomeAutomation?branchName=master)](https://becauseimclever.visualstudio.com/HomeAutomation/_build/latest?definitionId=2&branchName=master)

## Features

* Blazor user interface
* RESTful API for UI extensability
* MQTT backhaul for communicating with IoT devices
* MongoDb data store.

### General

This project is intended to run on a small Linux server, but when built from source, it can be run on any platform that is capable of running dotnet core.
The current deployment server has 2 phyical NICs, one as a DHCP client on the main home network, the second as a DHCP server, powering a seperate subnet exclusively for the IoT devices.

### Security

 ASP.NET Core with NGINX has built in support for TLS 1.2. The IoT devices are on a seperate subnet, and do not have direct internet connections. All IoT traffic is routed through the central server as an edge server, and any outgoing information is controlled via the server.

## Libraries in use

A big thank you to all the libraries we depend on for this project.

* AutoFixture - <https://github.com/AutoFixture/AutoFixture>
* Moq - <https://github.com/moq/moq4>
* xUnit - <https://github.com/xunit/xunit>

And a big thank you to the folks over at Studio 3T for providing me with a free license for their product.

## Getting setup for development

What you'll need:

* Visual Studio 2019 16.3 or newer (or VS code)
* DotNet Core 3.0
* MongoDB
  * Current settings file points to localhost:27017
  * Current version 4.0.6

### The Code

#### AutomationModels

This is where the core datamodels and view models will be stored. Right now there is no difference between the data models and the view models. 

#### AutomationRepositories

This is where the data access happens. All MongoDB code or any other data access from the API should go through this library.

#### AutomationLogic

This is where all of the domain logic for the API is located.

#### AutomationUI

This is a Blazor Web Assembly project. Right now the Blazor libraries are in preview, but are expected to be finalized this spring.

#### AutomationUnitTests

The Automated testing suite for all projects.

#### AutomationWebApi

This is the RESTful interface for consumption by the Blazor UI and any other UI interfaces on the main network.

#### DeviceBase

All devices must implement the interfaces here.

#### PowerStripPlugin

The first of the device projects. This will implement the interfaces from DeviceBase as necessary for interfacing with the physical PowerStrip device.
