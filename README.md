# Umbrella.Infrastructure.Firestore
Library to provide a client for GCP Firestore, with Repository Pattern implementation

Current Version: Stable - 1.0.0 - branch MASTER

[![Build Status](https://garaproject.visualstudio.com/UmbrellaFramework/_apis/build/status/Umbrella.Infrastructure.Firestore?branchName=main)](https://garaproject.visualstudio.com/UmbrellaFramework/_build/latest?definitionId=73&branchName=main)
- [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)

To install it, use proper command:
```
dotnet add package Umbrella.Infrastructure.Firestore 
```

For more details about download, see [NuGet Web Site](https://www.nuget.org/packages/Umbrella.Infrastructure.Firestore/)

<b>Branch Develop</b>
[![Build Status](https://garaproject.visualstudio.com/UmbrellaFramework/_apis/build/status/Umbrella.Infrastructure.Firestore?branchName=development)](https://garaproject.visualstudio.com/UmbrellaFramework/_build/latest?definitionId=73&branchName=development)

<b>Test Coverage</b>
You find test coverage report under _reports\codeCoverage_
TO run it locally, use the script: _scripts\runTestCoverage.bat_

remember to create a proper json file for credential to access GCP project.
please see class _src\Umbrella.Infrastructure.Firestore.Tests\CredentialManager.cs_