# _Hair Salon_

#### This application allows the user to create multiple stylist profiles, and add customer profiles to them, 12/2016

#### By **Brian Pritt**

## Description

This application creates a one to one database that links user-defined hair stylist objects to one or multiple customer objects.

_This application conforms to the following specs:_
* The application starts with an empty database
  * input: _null_
  * output: _null_
* The application saves stylist objects to the database
  * input: null - input -> {Terry}
  * output: {Terry}
* The application will find a specific stylist object in the database
  * input: - search - >{Chanda}
  * output: {Chanda}
* The application will edit a specific stylists information in the database
  * input: {Andre} - edit -> {Andrea}
  * output {Andrea}
* The application will delete a stylist object from the database
  * input: {Bill} {Jill} - Delete - > {Jill}
  * output {Bill}
* The application will perform all above tests for client objects

* The application will retrieve all customer objects under specific stylist
  * input: {Jane}
  * output: {Andy} {Cybil} {Jonas}
* The application will edit a specific customers information in the database
  * input: {Andre} - edit -> {Andrea}
  * output {Andrea}
* The application will delete a specific customer from the datatbase
  * input: {Bill} {Jill} - Delete - > {Jill}
  * output {Bill}
* The application will delete all stylist and customer information from database
  * input: Delete
  * output: _null_

## Setup/Installation Requirements

* This application relies on SSMS, Nancy, and other parts of the .NET framework
* _Database must be initialized, to do so from scratch you must create it using sqlcmd:_
  * In SQLCMD:
    * CREATE DATABASE hair_salon
    * GO
    * USE hair_salon
    * GO
    * CREATE TABLE stylist (id INT IDENTITY(1,1), name VARCHAR(255), phone VARCHAR(255), notes VARCHAR(255));
    * GO
    * CREATE TABLE customer (id INT IDENTITY(1,1), name VARCHAR(255), phone VARCHAR(255), last_visit VARCHAR(255), notes VARCHAR(255), stylist_id INT)
    * GO
* _To run application, machine must be running Windows with the latest .NET runtimes_
* _Clone this repository_
* _In terminal, navigate to project directory_
* _run > dnu restore_
* _run > dnx kestrel_
* _In browser window got to: localhost:5004/_


## Known Bugs

At time of commit, there were no known bugs

## Support and contact details

_For comments, questions and bug reports, visit project page:_
* https://github.com/brianpritt/cSharpHairSalon

My personal GitHub page
* https://github.com/brianpritt

## Technologies Used

This application relies on Microsoft .NET.  Other technologies include the Nancy Framework, and Razor view Engine.  HTML, CSS, and JavaScript are used as well.

### License

*Licensed unnder GPLv3*

Copyright (c) 2016 **_Brian Pritt_**
