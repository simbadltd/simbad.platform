# Simbad.Platform

[![Build Status](https://travis-ci.org/simbadltd/simbad.platform.svg?branch=master)](https://travis-ci.org/simbadltd/simbad.platform)

This project is the quintessence of the development experience accumulated over several years. In short, the problem that this project is designed to solve, arising from the fact that when you're starting any development, you often implement very similar functionality (boilerplate). It can take weeks or even months. This problem becomes even more when prototype/MVP development takes place. In this work every hour is on the account. There is no time, for example to be distracted by the implementation of the persistence aspect. You need to fully focus on verifying your idea or hypothesis. Or you're starting new application development and you've decided to reuse already tested and reliable platform for faster start with designing domain logic and entities. Here comes the Simbad.Platform.

## Installing

Simbad.Platform is a modular system separated by functionality into modules wrapped in nuget packages. So, to start with Simbad.Platform you just need to install necessary packages. The following set is a minimum to build fully-functional application:

```
PM> Install-Package Simbad.Platform.Core
PM> Install-Package Simbad.Platform.Persistence
```

## Getting Started

Once, all nuget packages have been installed, you can start building your app. First of all, you need to setup/customize your platform. All configuration code should be placed in the host assembly (where entry point of your application has been placed) and run on the start of your application before any other business logic run.

### Simplest configuration

```cs
  class Program
  {
      public static void Main (string[] args)
      {
          Global.Configure(); // It is the simplest configuration with all default configurations, simple event-bus and without persistence functionality
      }
  }
```

### Add business objects

To start building business objects you just need to inherit your future object from the platform abstraction:

```cs
  class FooBusinessObject : Substance.BusinessObject
  {
      // your logic goes here
  }
```

### Persistence

#### Use in-memory storage

```cs
  class Program
  {
      public static void Main (string[] args)
      {
          Global.Configure()
              .EnablePersistence(x => x.UseInMemoryStorage());
          
          // Add mappings between business objects and its` representation for persistence (dao)
          Mapping.Configure()
               .Add<FooBusinessObject, FooDao>();
      }
  }

```

#### Use sqlite storage

`Simbad.Platform.Persistence.Sqlite` package required.

```cs
  class Program
  {
      public static void Main (string[] args)
      {
          Global.Configure()
              .EnablePersistence(x => x.UseSqlite());
          
          // Add mappings between business objects and its` representation for persistence (dao)
          Mapping.Configure()
               .Add<FooBusinessObject, FooDao>();
      }
  }

```

#### Working with persistence

```cs
  class FooDomainService
  {
      public void Do()
      {
          var unitOfWork = new UnitOfWork(); // using UnitOfWork pattern [Martin Fowler, Patterns of Enterprise Application Architecture, 184]
          var repository = new Repository<FooBusinessObject>(unitOfWork);
          
          var entity = new FooBusinessObject();
          repository.Save(entity); // This statement just tracks our wish to save the object
          unitOfWork.Commit(); // This statement makes changes in the storage
          
          ...
          
          var entityFromStorage = _repository.FindSingle(x => x.Property == 42);
      }
  }
```

### TBD
Important that abstraction does not impose any restrictions on the design of the object. It only adds the necessary and properly designed aspects that will be useful. By default your objects will have:
* Unique identification controlled by the platform
* The ability to publish events and build event-driven business logic
