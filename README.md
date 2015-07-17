# FiVES WebTundra Synchronization

This Repo provides a set of libraries and plugins for the the FiVES Synchronization platform that implement the Tundra Synchronization Protocol. Tundra protocol is implemented as part of the RealXtend Tundra Synchronization platform that is published under Apache v2.0 license here:
* https://github.com/realXtend/tundra

The set of of provided Libraries allow communication with WebTundra browser clients. WebTundra is published under the Apache v2.0 License. For further information on the WebTundra project, please refer to:
* https://github.com/realXtend/WebTundra

The Serialization and protocol code is an extension to the SINFONI Middleware Project, that is published under the GNU LGPL v3 License:
* https://github.com/tospie/SINFONI

The Plugins and data model implementations are supposed to be run in FiVES, which is published under the GNU LGPL License:
* https://github.com/fives-team/fives

*FiVES, Tundra and SINFONI are part of the project __FIWARE__ of the European Union. Please find more information at __http://www.fiware.org__ . Tundra and FiVES are published there as reference implementation (Tundra) and alternative implementation (FiVES) of the Synchronization Generic Enabler*

## The Repository

This repository contains of the complete project in its root folder, and the following contents in the remaining folders:

* **Lib** : Precompiled Libraries of FiVES and SINFONI.
* **WTCommunication** : FiVES Plugin that implements the SINFONI Service interface that is used for WebTundra clients to connect to and exchange messages with FiVES
* **WTComponents** : Implementation of FiVES Components that match the Tundra ECA Domain Model
* **WTProtocol** : The SINFONI Protocol that implements Tundra Binary serialization and a subset of the Tundra Message Types

