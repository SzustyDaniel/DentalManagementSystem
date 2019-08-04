# Dental Management System
<p>DMS is a simple management system for the use of dental clinics. <br>
This project repository is for a software engineering course of software testing at Kinneret College.</p>

<h3>Instructions for running the solution</h3>
<h4>Requiered installetions:</h4>
<div>
  <ol><a href src="https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.300-windows-x64-installer">Install .Net Core 2.2 package</a></ol>
  <ol><a href src"https://dotnet.microsoft.com/download/thank-you/net48-developer-pack">Install .Net Framework 4.8</a></ol>
  <ol><a href src"https://go.microsoft.com/fwlink/?linkid=717179&clcid=0x409">Install  Microsoft Azure Storage Emulator</a></ol>
</div>

<h4>Startup of the projects</h4>
<div>
  <p>Before running the projects the azure storage emulator must run.<br>
  The Web API solution and the projects within it must run before any of the programs in the DMS solution.
  In the project properties setup multiple project startup for the ManagementService, QueueService and UsersService projects</p>
  <hr>
  <h5>UI Components</h5>
  <p>The DMS solution holds the UI components of the system.<br>
    To login to the Staff client application you need to enter the credentials that are stored in the data generators inside of UserService project. <br>
    To use the Queue display TV you need to load it before you log in to the staff client application. once you log in you will see the station displayed on the TV.<br>
    The Queue registering application works in the operational times of the clinic (it is set in the Management Service DataGenerator), if the Queue is not in the working hours it will be greyed out.
  </p>
</div>
