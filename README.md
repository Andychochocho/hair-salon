# Hair Salon

#### A site to test my skills for week three at Epicodus(Spring 2016 C# Cohort)

#### By Andrew Cho

## Description

_I used BDD in order to test every aspect of the logic until fully complete. I then utilized Nancy and Razor libraries to complete this project. I had used databases in this particular project in order to store my data in the server to later be called from within the site. I used the ideology of Restful Routing to implement my routing correctly._


## Setup/Installation Requirements
-Clone the repository.
-Use .sql files to implement the databases needed OR
-Use these command to create hairSalon and hairSalon_test
*
- CREATE DATABASE [hair_salon]
- GO
- USE [hair_salon]
- GO
 - CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255));
 - CREATE TABLE [dbo].[clients](
 - 	[id] [int] IDENTITY(1,1) NOT NULL,
 - 	[name] [varchar](255) NULL,
 - 	[appointment] [date] NULL,
 - 	[stylistId] [int] NULL
 - ) ON [PRIMARY]
 - GO

 - Use Nancy Web-Viewer
 - Create project through "dnu restore"
 - Run the project by calling "dnx kestrel"

## Known Bugs
_No known bugs!_

## Support and contact details
_If any contact is needed you can reach me at my email at cho-andrew@hotmail.com_

### License

*This software is licensed under the MIT license.*

Copyright (c) 2016 **By Andrew Cho**
