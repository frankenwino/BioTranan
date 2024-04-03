[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/TGpHSere)

# BioTranan

Mandatory independent submission assignment for BY-SUVNET OOP2

### Submission: April 3rd at 09:00

Mandatory code reviews will be conducted individually on the 3rd, 4th, and 5th of April.
The code reviews will focus on one or more questions similar to:

- What are the advantages and disadvantages of the project type you have chosen?
- Describe how API function X works from start to finish.
- Describe how system rule Y is implemented.

### Grading criteria

For G

- Asp.Net Core, EF Core, and SQLite are used.
- At least 3 out of 3 requirements for the website.
- At least 6 out of 9 requirements for the REST API.
- At least 2 out of 3 system rules.
- Completed the "Submission Checklist" at the bottom of this document.

For VG

- Emphasis on an overall good code structure.
- All basic requirements (G-requirements).
- At least 5 out of 7 additional requirements.
- Effective error handling of data.
- Handling of any newly arrived requirements changes.

**NOTE!** Also, read the text file TESTING.md! These requirements are necessary to pass the Testing course.

**Important!** Make sure to check off all the requirements you have succeeded with. Just put an x inside [ ] in this readme file to mark them off.

---

## Task description

You have been tasked with developing Bio Tranan's new website! Bio Tranan is a small non-profit cinema that screens a limited number of films. They have two auditoriums where they can screen movies.

Bio Tranan wants two things, firstly a public website where visitors should be able to:

- [x] View the schedule for upcoming movie screenings
- [x] Get basic information about the movies to be shown
- [x] Reserve seats for movie screenings

In addition to this, Bio Tranan, run by experienced developers, wants a REST API that can serve as an administration tool (in the future, they intend to develop their own frontend for this). The REST API should be able to:

- [x] Add new movies to be shown
- [x] Remove movies
- [x] Retrieve a list of available movies (This is not the same as showing the screening schedule!)
- [x] Add new auditoriums with a certain number of seats
- [x] Update and remove auditorium information
- [x] Add new screenings
- [x] Retrieve all **upcoming** screenings
- [x] List all reservations
- [x] List all reservations for a specific screening

Rules for the system

- [x] A movie can only be shown a certain number of times. Therefore, it should not be possible to add a movie to the screening schedule if it has already been shown the maximum number of times. This value is set when a movie is first added to the database.
- [x] It should not be possible to schedule a movie screening at the same time as another movie is being shown in the same auditorium.
- [x] It should not be possible to reserve seats for a screening if there are not enough seats available.

## In addition to these requirements, there are some extra requirements for the highest grade.

The website:

- [x] Display the total price before the reservation is made (see image)
- [x] Allow cancellation of a reservation in any way
- [x] Use an external API for something on the website, for example, a random advice from https://api.adviceslip.com/, or more advanced: https://developers.themoviedb.org/3/getting-started/introduction
- [ ] Reviews! It should be possible to rate movies and provide a written review by specifying your reservation code, but only after the movie has been shown.

REST API

- [x] Create a new reservation
- [x] It should be possible to add temporary seat restrictions to a specific screening, in case there are new pandemic regulations in the future
- [ ] Require authentication to be used (Optional method)
- [x] "Check in" a reservation code and mark it as used (Will be used during payment)

Rules for the system:

- [ ] Reservations older than one year should be automatically removed from the database. (Note to self: Service & API endpoint exist: DELETE {{Api_HostAddress}}/reservation/olderthan/1)

## The system

How you choose to structure this task is up to you, but you must, of course, use ASP.Net. Examples:

- Monolithic application in MVC, with the REST API in the same project.
- Blazor WASM as frontend and a single REST API as backend.
- Blazor Server with frontend-specific functionality and a separate REST API specifically for admins.

## Database

Use SQLite in your project that you commit. You can use InMemory and seeding while you develop, but SQLite must be used in the project you submit.

## Tips

- **Approach the project calmly and methodically! Don't do everything at once!**
- Read the requirements specification and analyze which objects you will need. Remember our previous exercises where we analyzed which nouns and verbs were included in the descriptions.
- It's okay if you create extra endpoints and classes beyond those described here, but these are the current minimum requirements.
- See this more as a mission from a client rather than an assignment. Don't be afraid to ask questions, propose alternative solutions if you think it would improve the project, or ask for explanations of why things should work according to the requirements specification.
- Keep in mind that the client may be an experienced purchaser of data systems, but the requirements may still be somewhat ambiguously formulated. It's possible to ask open clarification questions in the Questions channel on Discord!
- Testing is not part of this course, but if you're starting to feel a bit more comfortable with it, it's not a bad idea to implement tests in this project.

## Submission Checklist

- [x] I have removed unnecessary/unused code
- [x] I have removed unnecessary comments
- [x] I have formatted my code nicely
- [x] I understand what my code does (more or less). Try explaining aloud to yourself what your code does. Ask questions like "What happens when I add a movie?" "What happens if I try to reserve 2000 tickets for a screening?"
- [x] I have checked off which functional requirements I believe I have fulfilled by writing an x inside [ ] in this readme file.

![schema](schema.png)
![details](details.png)
![tack](tack.png)
