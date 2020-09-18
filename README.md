# README

## Markdown

This file is written in Markdown and is best viewed with a Markdown viewer

## Improvements list

* Could use Cucumber BDD to make test more easily readable ~ 5-6 hours
* Implement other Resource Test classes ~ 3 hours
  * UserResourceTests (nested albums, todos, posts)
  * AlbumResourceTests (nested photos)
* Implement Negative Tests Cases for above ~ 2 hours
* The `SetupContext` with `.Given()`, etc syntax repeats, not DRY, could be refactored with _systemUnderTest in `SetUp` ~ .5-1 hour
* TestData Provider could come from CSV or .xlsx files instead of code class ~ 1-2 hours
* Add HttpStatus codes to an enumeration instead of hardcoding integers like 201 (Created) ~ .5 hour
* Refactor .Header keys and values, add Json header strings to constants file ~ .25
* Performance testing using "total-call", "average-ttl-ms", etc ~ 1-2 hours
* Test API schema ~ 1 hour

## Problems Found

* Post returns 201 Created HTTP status even if no request Body is passed (blank)
* Post returns 201 Created HTTP status code if wrong datatypes are used in Body (request echo'ed back)
* No security on endpoints (should use tokens or rotating API keys)
* Security issues (see Line Breaks section below)
* Can be called from any domain (no CORS)
* No API versioning considered
* Get all posts seems dangerous for performance, could return too much data (should be paged) using `/posts`
* Even more concerned with `/comments` and `/photos` which return many more results

### Line Breaks

#### Example

```
HTTP GET
https://jsonplaceholder.typicode.com/comments/1

laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium
```

#### Concerns

Source: https://owasp.org/www-community/vulnerabilities/CRLF_Injection

* Line breaks in the body of the text (posts, comments, etc) use `/n`, which is not OS agnostic.
* In Windows both a CR and LF are required to note the end of a line.
* In Linux/UNIX a LF is only required. In the HTTP protocol, the CR-LF sequence is always used to terminate a line..
* Allowing line breaks to be submitted with posts or comments can result in a security vunrability CRLF Injection
  * This can occur in downstream processes where executable code is injected by an attacker after a CRLF

#### Remediation

* Suggest sanitizing inputs and outputs in API and not using a system specific line break

## Allure

Once the repo is downloaded install the Allure CLI with Scoop:

`scoop install allure`

To immediately view an Allure report (generate temp report directory):

`allure serve {allure-result directory}`

To generate an Allure report run the test suite, then: 

`allure generate {allure-result directory} -o {allure-report directory}`

Then to view the Allure report:

`allure open {allure-report directory}`

## Dependencies

* .NET Core 2.1
* Microsoft.NET.Test.Sdk
* RestAssured NuGet Package
* NUnit3
* NUnit Allure





