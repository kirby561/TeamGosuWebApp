# TeamGosuWebApp

## Description
This is a web application that implements the Team Gosu website at https://teamgosu.gg/. We are a Starcraft 2 clan mostly based in NA. Most coordination is done through our discord so if you're interested in learning more about the team, joining, or have questions, join our discord at https://discord.teamgosu.gg/

## Reporting Issues
Write up issues on the github issue tracker (https://github.com/kirby561/TeamGosuWebApp/issues) or report them in the Discord.

## Building
The project is written in C# based off dotnet 5. To build it:
1. Download Visual Studio Community 2022 or later at https://visualstudio.microsoft.com/vs/community/
2. Open TeamGosuWebApp.sln
3. Build and run.
4. Open http://localhost:9998/ in a browser.

## Contributing
1. Write an issue or pickup one in the github issue tracker.
2. Make sure your issue is opened to you.
3. Implement your change following the coding conventions in this readme.
4. Submit a pull request for review.
5. Iterate on feedback as needed.

## Coding Conventions
Follow mainstream C# coding conventions at https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions 
with the following exceptions:
1. Private/Protected member variables should be prefixed with "_" (for example: _someMemberVariable). This makes them stick out in diff tools and prevents shadowing.
2. Compact bracing is preferred rather than a brace being on its own line. For example:
>	if (...) {
>		...
>	} else {
>		...
>	}

## Icon Credits:
	Rules icon - [Rules icons created by Kiranshastry - Flaticon](https://www.flaticon.com/free-icons/rules)
