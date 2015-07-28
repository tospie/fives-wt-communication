# FiVES WebTundra Synchronization

This Repo provides a set of libraries and plugins for the the FiVES Synchronization platform that implement the Tundra Synchronization Protocol. Tundra protocol is implemented as part of the RealXtend Tundra Synchronization platform that is published under Apache v2.0 license here:
* https://github.com/realXtend/tundra

The set of of provided Libraries allow communication with WebTundra browser clients. WebTundra is published under the Apache v2.0 License. For further information on the WebTundra project, please refer to:
* https://github.com/realXtend/WebTundra

The Serialization and protocol code is an extension to the SINFONI Middleware Project, that is published under the GNU LGPL v3 License:
* https://github.com/tospie/SINFONI

The Plugins and data model implementations are supposed to be run in FiVES, which is published under the GNU LGPL v3 License:
* https://github.com/fives-team/fives

*FiVES, Tundra and SINFONI are part of the project __FIWARE__ of the European Union. Please find more information at __http://www.fiware.org__ . Tundra and FiVES are published there as reference implementation (Tundra) and alternative implementation (FiVES) of the Synchronization Generic Enabler*

## The Repository

This repository contains of the complete project in its root folder, and the following contents in the remaining folders:

* **Lib** : Precompiled Libraries of FiVES and SINFONI.
* **WTCommunication** : FiVES Plugin that implements the SINFONI Service interface that is used for WebTundra clients to connect to and exchange messages with FiVES
* **WTComponents** : Implementation of FiVES Components that match the Tundra ECA Domain Model
* **WTProtocol** : The SINFONI Protocol that implements Tundra Binary serialization and a subset of the Tundra Message Types

## Quickstart Guide

This repo comes with all code files needed to compile the binary version of the respective FiVES plugins and protocols, as well as configuration files for FiVES.
 1. Clone this repository and build the entire solution.
 2. Find your compiled plugins together with all dependencies and configuration files in the */Build* folder
 3. Copy these into the *Plugin Path* of your FiVES deployment. Allow overwriting existing files.
 4. Copy the file *WTProtocol.dll* into the SINFONI protocol path of your deployment. You can find this file in the SINFONI configuration file *SINFONIPlugin.dll.config*
 4. Make sure that *FiVES.exe.config* is located in the same directory as *FiVES.exe*
 5. Make sure that the other config files are contained in the same directory as the respective plugins
 6. Run FiVES as usual. FiVES will launch with all plugins needed for WebTundra to connect

## Limitations

The Tundra protocol is a complex wire protocol that is closely coupled to the Tundra ECA model implementation. This means in particular that components and attribute types have to match the exact implementation of the Tundra types to be serialized and deserialized correctly.

Because of that, this set of libraries is not - and does not intend to be - a full replacement of Tundra as synchronization server for WebTundra. It does rather provide a minimal set of message types and component types that are supported to run synchronized 3D applications based on WebTundra and FiVES.

You are of course encouraged to extend the set of supported message types and data as you need in your application.

Currently supported are the following mechanisms:

* __Data Serialization__ : Serialization and Deserialization of the base types: 
  * _1: string_
  * _2: int_
  * _3: real_
  * _8: bool_
  * _11: assetReference_
  * _12: assetReferenceList_
  * _13: entityReference_
  * _16: transform_
* __Components__ :
  * _17: Mesh_
  * _20: Placeable_
* __Message Types__ :
  * _100: Login_
  * _101: LoginReply_
  * _110: CreateEntity_
  * _113: EditAttributes_
  * _116: RemoveEntity_
