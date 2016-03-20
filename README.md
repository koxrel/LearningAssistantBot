# LearningAssistantBot
##Intro
The aim of the project has been the development of a Telegram Bot with the capability to remind users of upcoming deadlines and home tasks scheduled for practical sessions. The app is essentially split into two parts: a control panel and a module responsible for the bot operation, run in parallel. 

All information is stored in Microsoft SQL database (for the simplicity, we have used the localdb server). A GUI is built on WPF utilising MVVM pattern.

The team consists of two members:
* Igor Tresoumov aka koxrel (responsible for API Interaction, Database implementation and Data Access Layer);
* Sergey Pavlov aka CapObvios (responsible for GUI and user interaction with model classes).

##Setup
The app is ready to run. However, one should create and populate a database using "Update-Database" from Package Manager Console.

**Important Note!** In Package Manager Console, select LearningAssistant.Database as default project.
