# QA Hub
### Author: Brian Pritt
### November 2019

__ToDo:__
* add error handling to all routes
* add authentication
* finish user controller
* Handle error when trying to delete Tickets and Replies that do not exist.

### About

ROUTES
Routes use the following template

_/{controller}/{id}/action_

__Ticket Controller__
GET `/tickets` : Get all tickets
POST `/tickets/new` : Post new ticket
GET `/tickets/{id}` : Get one ticket with {id} and corresponding Replies
PUT `/tickets/{id}/update` : Update ticket with {id}
DELETE `/tickets/{id}/delete` : Delete ticket with {id} and corresponding Replies


__Replies Controller__
GET `/replies` : Get all replies (not a useful route, I may cut off this endpoint but leave method)
POST `/replies/{id}` : Post new reply to ticket with {id}
GET `/replies/{id}` : Get reply with {id} (also not a really useful route, maybe for user instead)
PUT `/replies/{id}/update` : Update reply with {id}
DELETE `/replies/{id}/delete` : Delete Reply with {id}

__Parameters and Body__
QA Hub api follows CRUD conventions.  Identifying information such as `id` and `actions` are passed through URL, and query bodies are passed in JSON format.
### Ticket:
In order to successfully submit a ticket, the following fields must be present:
`tickettitle, ticketcategory, ticketbody, ticketauthor`
TicketTime and TicketUpdate are automatically generatend upon creation.

Only three fields are available on PUT request:
`titckettitle, ticketcategory, ticketbody`
TicketUpdate will be updated automatically. Not all fields are required for successful PUT.

####Sample `ticket` POST:
`/tickets/new`
curl `curl --header "Content-Type: application/jsequest POST   --data '{"tickettitle":"test","ticketauthor":"test","ticketcategory":"UI","ticketbody":"body"}'   http://localhost:5000/tickets
`
```
{
    "tickettitle": "Submit CTA on main page must be tapped three times before submit action",
    "ticketcategory": "UI",
    "ticketbody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
    "ticketauthor": "Brian"
}
```

#### Sample `ticket` PUT:
`/tickets/{id}/update`
```
{
    "ticketbody": "EDIT: CTA is now non-functional"
}
```
#### Sample `ticket GET`
`/tickets`
curl `curl http://localhost:5000/tickets`
```
[
    {
        "ticketId": 17,
        "ticketTitle": "Submit CTA on main page must be tapped three times before submit action",
        "ticketCategory": "UI",
        "ticketBody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
        "ticketAuthor": "Brian",
        "ticketTime": "2019-11-25T19:51:59",
        "ticketUpdate": "2019-11-25T19:51:59",
        "ticketReplies": []
    }
]
```
`/tickets/{id}`
curl `curl http://localhost:5000/tickets/{id}`
```
[
    {
        "ticketId": 17,
        "ticketTitle": "Submit CTA on main page must be tapped three times before submit action",
        "ticketCategory": "UI",
        "ticketBody": "After redirection to home page when creating a new account, submit CTA must be tapped three times in order for rom to submit.",
        "ticketAuthor": "Brian",
        "ticketTime": "2019-11-25T19:51:59",
        "ticketUpdate": "2019-11-25T19:51:59",
        "ticketReplies": [
            {
                "replyId": 13,
                "replyAuthor": "Brooke",
                "replyBody": "I have experienced this issue, when using submit directly after login as well",
                "replyTime": "2019-11-25T19:53:40",
                "replyUpdate": "0001-01-01T00:00:00",
                "ticketId": 17
            }
        ]
    }
]
```
