# Homework #3 : Design Principles: SOLID

## Design Principles: Design Patterns - SOLID

Refactor (add classes/method signatures) the code submitted in the previous assignment to include the following functionality:

1. Users to login/start FileSystem
2. Users to create new directories and store them
3. Users to list/read existing directories (with permissions)
4. Users to create new files in directories and store them
5. Users to list existing files (with permissions)
6. Users to set/edit permissions on files and directories

Please specify what parts of your code are using the objects that you have already defined.

## Rubric

1. **User Interface/API classes/methods** (1.5 point)
2. **Persistence classes/methods** (1.5 point)
3. **Judicious use of SOLID principles** (2 point)






Abstract Factory Pattern
Strategy Pattern

SOLID Principles
Single Responsibility Principle
Open/Closed Principle
Liskov Substitution Principle
Interface Segregation Principle
Dependency Inversion Principle




### User Story #1
**As a** user,  
**I want to** be able to login/start FileSystem,  
**So that** I can access and manage my files and directories securely.

### User Story #2
**As a** user,  
**I want to** be able to create new directories and store them,  
**So that** I can organize my files more effectively.

### User Story #3
**As a** user,  
**I want to** be able to list/read existing directories based on my permissions,  
**So that** I can access the directories I have permission to view.

### User Story #4
**As a** user,  
**I want to** be able to create new files in directories and store them,  
**So that** I can manage my files in a way that suits my needs.

### User Story #5
**As a** user,  
**I want to** be able to list existing files based on my permissions,  
**So that** I can access the files I have permission to view.

### User Story #6
**As a** user,  
**I want to** be able to set/edit permissions on files and directories,  
**So that** I can control who has access to certain files and directories.



### User Story #1
**Title**: User Login to FileSystem

**As a** registered user,  
**I want to** be able to login/start FileSystem,  
**So that** I can access and manage my files and directories securely.

**Acceptance Criteria:**

1. **Given** I am a registered user, **when** I provide my correct username and password, **then** the FileSystem should start, and I should be able to access my personal workspace within a reasonable timeframe (i.e., under 3 seconds under normal network conditions).
2. **Given** I am a registered user, **when** I provide incorrect login credentials, **then** the FileSystem should show an error message with guidance for corrective action and ask me to try logging in again.
3. **Given** I am a registered user and I enter an incorrect password, **when** I attempt to login multiple times (e.g., more than 5 times) in a short period, **then** my account should be temporarily locked as a security measure, and I should be notified via my registered email.
4. **Given** I am a registered user, **when** I forget my password, **then** I should be able to use a 'Forgot Password' function to reset my password via my registered email. I should receive the reset password email within a reasonable timeframe (e.g., 5 minutes).
5. **Given** I am a registered user, **when** the FileSystem starts, **then** I should see my previously opened files and directories unless I have manually closed them in the last session.
6. **Given** I am a registered user, **when** I have successfully logged in, **then** I should be able to log out at any time, and my current session should end immediately after logging out.
7. **Given** I am a logged-in user, **when** there's no activity on my session for an extended period (e.g., 15 minutes), **then** the FileSystem should automatically log me out to preserve my security.

**Notes:**
- User login information must be encrypted and stored securely.
- The FileSystem must have a timeout feature for user inactivity to ensure security.
- All system messages should be user-friendly and guide the user on what action to take next.
- Consider accessibility guidelines in the design of error messages and guidance.



### User Story #2
**Title**: User Password Recovery

**As a** registered user,  
**I want to** be able to use a 'Forgot Password' function,  
**So that** I can reset my password via my registered email if I forget it.

**Acceptance Criteria:**
1. **Given** I am on the login page and have forgotten my password, **when** I click on 'Forgot Password', **then** I should be prompted to enter my registered email.
2. **Given** I have entered my registered email, **when** I submit the 'Forgot Password' request, **then** I should receive an email with a password reset link within a reasonable timeframe (e.g., 5 minutes).
3. **Given** I have received the password reset email, **when** I click on the link, **then** I should be redirected to a secure page where I can reset my password.

### User Story #3
**Title**: Automatic Logout due to User Inactivity

**As a** logged-in user,  
**I want to** be automatically logged out after a period of inactivity,  
**So that** my session remains secure if I forget to log out.

**Acceptance Criteria:**
1. **Given** I am a logged-in user, **when** there's no activity on my session for an extended period (e.g., 15 minutes), **then** the FileSystem should automatically log me out.
2. **Given** I am a logged-in user, **when** I am automatically logged out due to inactivity, **then** I should see a message informing me that I have been logged out due to inactivity next time I visit the login page.

### User Story #4
**Title**: Account Locking After Multiple Unsuccessful Login Attempts

**As a** registered user,  
**I want to** have my account temporarily locked after multiple unsuccessful login attempts,  
**So that** my account is protected against unauthorized access attempts.

**Acceptance Criteria:**
1. **Given** I am a registered user and I enter an incorrect password, **when** I attempt to login more than 5 times in a short period, **then** my account should be temporarily locked.
2. **Given** my account is temporarily locked, **when** I attempt to login again, **then** I should see a message informing me that my account is locked.
3. **Given** my account is temporarily locked, **when** it gets locked, **then** I should receive an email informing me of this security measure and how to unlock it.
