SOLID principles provide guidance for designing software that is easy to manage, maintain, and scale. The principles include:

Single Responsibility Principle (SRP)
Open-Closed Principle (OCP)
Liskov Substitution Principle (LSP)
Interface Segregation Principle (ISP)
Dependency Inversion Principle (DIP)
Here's how you can refactor the design using SOLID principles:

Single Responsibility Principle (SRP): The FileSystem class currently handles multiple responsibilities (creating, deleting, updating files and directories, managing permissions, etc.). We should split these responsibilities into different classes.

Open-Closed Principle (OCP): We should be able to add new functionality or change existing functionality without modifying existing code (using inheritance and/or interfaces).

Liskov Substitution Principle (LSP): We should be able to use a child class in place of a parent class and it should behave correctly.

Interface Segregation Principle (ISP): Clients should not be forced to depend on interfaces they do not use.

Dependency Inversion Principle (DIP): High-level modules should not depend on low-level modules; both should depend on abstractions.