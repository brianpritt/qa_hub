# QA Hub
### Author: Brian Pritt
### November 2019

__About__
QAHub is a simple QA tracking API.
It is not weighed down with bells and whistles, and most likely will not add to the richness of your current QA paradigm.

QAHub is built with MySql 8 and .Net Core 3.0

__Requirements:__
* MySql / MySql Workbench 8.0
* Microsoft .Net Core 3.x

__To install and use:__
* Clone repository: https://github.com/brianpritt/qa_hub
* In project directory run `dotnet restore`
* In MySql Workbench, run the script found in `qa_hub_mysql.sql`
* Open `Startup.cs` in project directory and modify the following line to match your connection settings: `public static string ConnectionString = "server={your local host};user id={your user id};password={your password};port=3306;database=qa_hub_sql;";`
* In project directory enter the following `dotnet run`


__ToDo:__
* add error handling to all routes
* add authentication
* Handle error when trying to delete Tickets and Replies that do not exist.
* when auth is finished, allow all with administrative rights to add to Assignments array (make List<> instead), and Teams array).
    * this will require a controller for administrative queries/methods.
* add to REPORTS CONTROLLER 
    * user statistics
    * ticket time statistics
* assign tickets to user

__Known issues/features?__
* Polymorphic Associations
* Tickets and replies can be made by non-existent users.
* When querying a single user, a list of tickets will populate with an empty list of replies.  Not sure if I should attempt to remove it.
* Currently, because of foreign key association, a single user cannot be queried unless they have both tickets and repies active.
* using INNER JOIN on GET one ticket returns only tickets with replies, LEFT JOIN returns null on the replies that do not exist and I have not figured out how to handle.
* queries such as GET /tickets will currently return all tickets, but not their replies.  Reply list appears in the response but is empty.  It's not decided if all replies will be added to a GETALL for tickets

    * the same issue appears when querying ALL users, tickets and replies appear as empty lists.  When a single user is queried, the replies and tickets lists are populated.
* DELETE on ticket queries db twice.  IT's a challenge to remove a ticket and reply AND remove a ticket with no replies with one query.

### About

ROUTES
Routes use the following template

_/{controller}/{id}/action_

__Ticket Controller__
GET `/tickets` : Get all tickets  
POST `/tickets/` : Post new ticket  
GET `/tickets/{id}` : Get one ticket with {id} and corresponding Replies  
PUT `/tickets/{id}/update` : Update ticket with {id}  
DELETE `/tickets/{id}/delete` : Delete ticket with {id} and corresponding Replies  

_Parameters:_  
* assignment  
    * `/tickets/?assignment=ui` will return all tickets that are assigned to UI team  
    * current teams are `platform`, `ui`, `data`, and `unassigned`.  When Identity is implemented, this will be change from an array to List that can be updated by user with administrative privileges.  
    * a ticket that does not match one of these teams or is not provided will automatically be added to unassigned.  This can be updated in the Update route.   
* notes:  
    * When a ticket is Deleted, all corresponding replies will be deleted as well.  

__Replies Controller__  
GET `/replies` : Get all replies (not a useful route, I may cut off this endpoint but leave method)  
POST `/replies/{id}` : Post new reply to ticket with {id}  
GET `/replies/{id}` : Get reply with {id} (also not a really useful route, maybe for user instead)  
PUT `/replies/{id}/update` : Update reply with {id}  
DELETE `/replies/{id}/delete` : Delete Reply with {id}  

__Users Controller__  
GET `/users` : Get all users   
POST `/users/{id}` : Post new user  
GET `/users/{id}` : Get user and all tickets and replies associated  
PUT `/users/{id}/update` : Update user with {id}  
DELETE `/replies/{id}/delete` : Delete Reply with {id} - does not delete tickets and replies associated with user  

* notes:
    * When a user is deleted, userid on both ticket and reply table are replaced with -1.  In production this can be replaced with a specific userid. This way tickets are retained even after a user is removed.  This can be reassigned through each tables' respective PUT routes.  

__Reports Controller__  
GET `/reports` : get a general report on all users, tickets, and replies.   

* notes:
    * controller not fully implemented past returning a dictionary of numbers.  

__Parameters and Body__  
QA Hub api follows CRUD conventions.  Identifying information such as `id` and `actions` are passed through URL, and query bodies are passed in JSON format.  

### Ticket:  
In order to successfully submit a ticket, the following fields must be present:  
`tickettitle, ticketcategory, ticketbody, ticketauthor`  
TicketTime and TicketUpdate are automatically generatend upon creation.  
`ticketcategory` must match one of the teams available, or else with be givien the value `unassigned`  

Only three fields are available on PUT request:
`titckettitle, ticketcategory, ticketbody`
TicketUpdate will be updated automatically. Not all fields are required for successful PUT.

#### Sample `ticket` POST:  
`/tickets/`  
__Curl__  
`curl --header "Content-Type: application/json" -X POST  --data '{"tickettitle":"Submit CTA on main page must be tapped three times before submit action","ticketauthor":1,"ticketcategory":"UI","ticketbody":"After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit."}'   http://localhost:{port-number}/tickets  
`  

__Raw JSON Body__   
```
{
    "tickettitle": "Submit CTA on main page must be tapped three times before submit action",
    "ticketcategory": "UI",
    "ticketbody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
    "ticketauthor": 1
}
```

#### Sample `ticket` PUT:  
`/tickets/{id}/update`  
__Curl__  

`curl --header "Content-Type: application/json" -X POST   --data '{"ticketbody":"EDIT: CTA is now non-functional"}'   http://localhost:{port-number}/tickets`

__Raw JSON Body__  
```
{
    "ticketbody": "EDIT: CTA is now non-functional"
}
```
#### Sample `ticket GET`  
`/tickets`
__Curl__  
`curl http://localhost:5000/tickets`
__Raw JSON Body__  
```
[
    {
        "ticketId": 17,
        "ticketTitle": "Submit CTA on main page must be tapped three times before submit action",
        "ticketCategory": "UI",
        "ticketBody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
        "ticketAuthor": 4,
        "ticketTime": "2019-11-25T19:51:59",
        "ticketUpdate": "2019-11-25T19:51:59",
        "ticketReplies": []
    }
]
```
#### Sample Ticket Get (One Ticket)  
`/tickets/{id}`  
__Curl__
`curl http://localhost:5000/tickets/{id}`  
__Raw JSON Body__  
```
[
    {
        "ticketId": 17,
        "ticketTitle": "Submit CTA on main page must be tapped three times before submit action",
        "ticketCategory": "UI",
        "ticketBody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
        "ticketAuthor": 210,
        "ticketTime": "2019-11-25T19:51:59",
        "ticketUpdate": "2019-11-25T19:51:59",
        "ticketReplies": [
            {
                "replyId": 13,
                "replyAuthor": 45,
                "replyBody": "I have experienced this issue, when using submit directly after login as well",
                "replyTime": "2019-11-25T19:53:40",
                "replyUpdate": "0001-01-01T00:00:00",
                "ticketId": 17
            }
        ]
    }
]
```
### Reply  

In order to successfully add a new reply the following must be present in body:  
`replyauthor` and `replybody`  
and the ticketid must be present as a parameter, as such:
`replies/17`  
Only one field is available on PUT: `replybody`  

#### Sample `Reply` Post  
`/replies/{ticketId}`  

`curl --header "Content-Type: application/json" -X POST  --data'{"replyauthor":2,"replybody":"This is the wackiest error"}'   http://localhost:{port-number}/replies/{ticket-id}
`

__Raw JSON Body__   
```
{
    "tickettitle": "Submit CTA on main page must be tapped three times before submit action",
    "ticketcategory": "UI",
    "ticketbody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
    "ticketauthor": 1
}
```
#### Sample `Reply` Put  