# CastZone
A Podcast portal and hub for discover podcasts.

## Modules
The portal has this modules:
* _Importer_: Will import the podcasts from iTunes;
* _Updater_: Update the episodes from podcast feed;
* _Feed_: Will provide the feeds
* _Admin API_ 
* _Admin_: Management user interface to select and edit 
the podcasts
* _Web API_ 
* _Web_: The portal interface

## Architecture
This project is using MicroService Architecture, using 
Docker and NSQ for message services.

## Install & Running
[Docker](https://docker.com) and [Docker Compose]() is required to run the project. 
After install and pulling the source code, run the following commands:
```bash
docker network create nsq
docker network create mongo

cd server

docker-compose up -d
cd ..
docker-compose up -d
```

## Coding
Clone the source code and run:
```bash
sh install-dependence.sh
```