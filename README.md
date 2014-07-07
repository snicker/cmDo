# cmDo

cmDo is a lightweight, single-executable, portable, Windows CLI utility to interface with the ToodleDo API including Growl support.

You can add ToodleDo tasks from the command line. It's small.

---
## Configuration

cmDo must be initially configured with your ToodleDo log in and password. Call cmDo from the command line with the following parameters:
`cmDo -config u:<username@domain.tld> p:<mytoodledopassword>`

Credentials are stored in the Windows password vault, not in plain text, so don't worry.

---
## Usage

cmDo is only used to send tasks to ToodleDo. It cannot retrieve tasks at this time.

cmDo is called from the command line with the title for the task as parameters...

`cmdo this is my new task`

...will add a new task called 'this is my new task'.

### Syntax

Most of the syntax in the [ToodleDo Email Syntax](http://www.toodledo.com/info/help_email.php) docs is supported. Notable differences:

* use `++` for notes.
* dates and times can be fuzzy matched for the most part. `#tuesday` will set a due date of Tuesday.
* cmDo will try to partial match for contexts and folders. If you have a context called "Phone Calls" and use `@calls`, the task will get assigned to your "Phone Calls" context.
* creating a task with `wf`, `waiting for`, or `waiting on` in the title will automatically assign a status of Waiting for the task. `cmdo WF Steve to call me back #monday` will create a task with a due date of Monday and a status of 'Waiting'

### Other Features

* Default Task Values

  You can define defaults for newly created tasks by using the `-setdefault` parameter.

  `cmdo -setdefault @work $planning`

  will create any new tasks with the context of 'work' and a status of 'planning'.

* Piped Input

  If you need to pipe input to cmDo, you must use the `-pipe` parameter. I haven't found a way to automatically detect if input is being piped to the application in C#.

  `type todo.txt | cmdo -pipe`

  will send each line in `todo.txt` to ToodleDo.

* Growl for Windows support

  cmDo will attempt to use Growl to notify you of messages. If Growl isn't running, it uses obnoxious message boxes. I should probably change that. Or you can, by submitting a pull request!

---
## About

I've been using cmDo for the last 4 or 5 years all by my lonesome, always with the intent of putting it up somewhere, so now I finally did.

My workflow consists of Launchy + cmDo for getting things into ToodleDo as soon as possible. I have cmDo bound to a 'do' command in Launchy, so keystroke combos of `<ctrl + space> do <tab> new task name @context *project #tomorrow` are I do multiple times on a daily basis.

---
## Contributing

Feel free to fork and submit pull requests. Project was last built in Visual C# Express, I think.