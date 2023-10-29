# Project description
This app should help us manage translators and jobs they work on. 
It is currently a working proof of concept but it needs a bit of polishing and a couple of features. 
It should not take more than a few hours to complete. 
We prefer to see great quality of code rather than great number of features. Don't worry if you don't find the time to implement everything.
No backwards compatibility required, feel free to adjust the project in any way or use any library you wish.

# Requirements - API 
We do expect this app to grow in the future and your design choices should reflect that.

Your tasks are following (ordered by importance): 

- **Refactor the code so it is of production quality**
> * DO try to outline what you would consider a good architecture
> * DO consider adding folders, projects, interfaces, design patterns, whatever you see fit
> * we'd love to see code that is easy to read, extend and maintain
> * maybe next feature request will be to support creating jobs by more file types or assigning jobs via messaging

- **RESTful api design** 

- **Cover with tests**
> - DO NOT worry about code coverage, we want to see *how* you write tests, not that you can write many
> - DO cover the important parts first

- **Implement additional features [optional, for additional points]**
> - business rule: "only Certified translators can work on jobs"
> - implement endpoints that will allow to track (set and get) which translator works on what job

# Requirements - front end (if that is also your domain)
Create a super simple frontend. We would like to see a few components, a bit of data manipulation and a small state management. 
Our tools of choice are React (CRA) + Typescript but use what you prefer
- DO NOT bother with styling, the uglier the better
- DO NOT implement full functionality of the backend, pick a small part of the functionality
- maybe just allow to manage translators or visualize the job state

# Do not worry about
- all parts that are common functionality: https, authorization, logging, database location...
- implementing everything perfectly, rather do smaller scope well and we can discuss the rest in person

# Deliverables
Clone/fork to your repo and deliver as a link to your repo or share the `git-archive`. 
Commit to master, follow usual git culture. 
Please include a note regarding how to run.
