# Umbrella.Infrastructure.Firestore
Library to provide a client for GCP Firestore, with Repository Pattern implementation


[![Build Status](https://garaproject.visualstudio.com/UmbrellaFramework/_apis/build/status/Umbrella.Infrastructure.FileStorage?branchName=main)](https://garaproject.visualstudio.com/UmbrellaFramework/_build/latest?definitionId=81&branchName=main)

- [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)
- [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Umbrella.Infrastructure.Firestore&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Umbrella.Infrastructure.Firestore)

[![Nuget](https://img.shields.io/nuget/v/Umbrella.Infrastructure.Firestore.svg?style=plastic)](https://www.nuget.org/packages/Umbrella.Infrastructure.Firestore/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Umbrella.Infrastructure.Firestore.svg)](https://www.nuget.org/packages/Umbrella.Infrastructure.Firestore/)


To install it, use proper command:

```
dotnet add package Umbrella.Infrastructure.Firestore 
```

For more details about download, see [NuGet Web Site](https://www.nuget.org/packages/Umbrella.Infrastructure.Firestore/)

<b>Branch Develop</b>: ![Build Status](https://garaproject.visualstudio.com/UmbrellaFramework/_apis/build/status/Umbrella.Infrastructure.Firestore?branchName=development)](https://garaproject.visualstudio.com/UmbrellaFramework/_build/latest?definitionId=73&branchName=development)

<b>Test Coverage</b>
You find test coverage report under _reports\codeCoverage_ .

To run it locally, use the script: _scripts\runTestCoverage.bat_

remember to create a proper json file for credential to access GCP project.

please see class _src\Umbrella.Infrastructure.Firestore.Tests\CredentialManager.cs_

# HOWTO implementing repository
to implement the repository you have to follow the below steps.

## Create a firestore document class
create a class to map the firestore document to be store on database; remember to inherit from _FirestoreDocument_:

```c#
    /// <summary>
    /// class to map a firestore document for Diary
    /// </summary>
    public class DiaryDocument : FirestoreDocument
    {
        [FirestoreProperty]
        public string Username {get; set;}

        /// <summary>
        /// Default Constr
        /// </summary>
        /// <returns></returns>
        public DiaryDocument() : base()
        {
            this.Username = "";
        }
    }
```

each property you add must to be decorated with attribute _FirestoreProperty_.

## Create a Mapper class

create a class to map the firestore document to the entity and viceversa, inheriting from _IFirestoreDocMapper_:

```c#
    /// <summary>
    /// Mapper between the firestore document <see cref="DiaryDocument"/> and you domain entity or Dto <see cref="DiaryDto"/> 
    /// </summary>
    public class DiaryDocumentMapper : IFirestoreDocMapper<DiaryDto, DiaryDocument>
    {
        /// <summary>
        /// Default constr
        /// </summary>
        public DiaryDocumentMapper()
        {
        }

        public DiaryDto FromFirestoreDoc(DiaryDocument doc)
        {
            if (doc == null)
                return null;

            var dto = new DiaryDto();

            . . .

            return dto
        }

        public DiaryDocument ToFirestoreDocument(DiaryDto dto)
        {
            if (dto == null)
                return null;

            var doc = new DiaryDocument();
            doc.SetDocumentId(dto.ID);
            doc.CreatedOn = dto.CreatedOn.ToFirestoreTimeStamp()
            doc.LastUpdatedOn = dto.LastUpdatedOn.ToFirestoreNullableTimeStamp();

            . . .

            return dto
        }
    }
```

Please notice that it's necessary to invoke extension _ToFirestoreTimeStamp_ or _ToFirestoreNullableTimeStamp_ to get right timestamp data.

## Create the repository

To create a repository for your dto class, you have ho ineherit from proper class:

```c#
    /// <summary>
    /// Implementation of repository pattern on Firestore
    /// </summary>
    public class FirestoreDiaryRepository : ModelEntityRepository<DiaryDto, DiaryDocument>
    {
        /// <summary>
        /// Default constr
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="projectId">GCP project Id where firestore DB has been provisioned</param>
        /// <param name="dotnetEnv">Code of environment</param>
        /// <param name="mapper">mapper to translate firestore document to DTO and viceversa</param>
        public FirestoreDiaryRepository(ILogger logger, string projectId, string dotnetEnv, IFirestoreDocMapper<DiaryDto, DiaryDocument> mapper) 
            : base(logger, projectId, dotnetEnv, false, "HeadacheDiary", mapper)
        {
        }
    }
```

Please notice that:

- autoGenerateId: TRUE to generate id of object; FALSE to use the id set outside the repo
- collectionName: name of collection to save data on Firestore instance
