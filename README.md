# CogniVault

## Purpose
CogniVault is a file system application designed to provide a secure and efficient storage solution for personal and business data. The primary purpose of CogniVault is to offer a reliable and user-friendly platform for organizing, managing, and accessing files and directories.

## Goals
- **Secure Data Storage**: CogniVault aims to ensure the security and confidentiality of user data. It employs robust encryption algorithms and implements access controls to protect sensitive information from unauthorized access.
- **Efficient File Organization**: CogniVault provides an intuitive and efficient file organization system. Users can easily create, rename, and organize files and directories within a hierarchical structure, making it simple to navigate and locate specific files or folders.
- **Reliable Data Backup and Recovery**: CogniVault offers reliable data backup and recovery features. It automatically creates periodic backups of user data, minimizing the risk of data loss. In the event of accidental deletion or system failure, users can easily restore their files from previous backups.
- **Advanced Search and Filter Options**: CogniVault includes advanced search and filter capabilities to help users quickly locate specific files based on various criteria such as name, file type, creation date, or tags. This saves time and improves overall productivity.
- **Collaboration and File Sharing**: CogniVault facilitates collaboration by allowing users to share files and directories with others securely. It supports different access levels, enabling users to grant read-only or read-write permissions to collaborators.
- **Cross-Platform Compatibility**: CogniVault is designed to be compatible with multiple platforms and devices. It supports seamless synchronization and access to files across desktops, laptops, smartphones, and tablets, ensuring a consistent user experience.

## Deployment

### Requirements to run the demo
Requires:
* [VS Code](https://code.visualstudio.com/download)
* [.NET Core SDK](https://dotnet.microsoft.com/download)

### Run
* Clone the repository
* Run nuke command to output jwt token to be used for signing
* Open
* Run

## Appsettings Explained
* `JwtSettings` - Settings for JWT token generation
  * `Secret` - Secret used to sign the JWT token
  * `Issuer` - Issuer of the JWT token
  * `Audience` - Audience of the JWT token
  * `Expiration` - Expiration of the JWT token

## Usage
Provide instructions on how to use your application, including installation steps, configuration settings, and any other relevant details to get started with the application.

## Contributing
Specify guidelines for contributing to your application, such as how to report bugs, submit feature requests, or contribute code. Mention the preferred workflow and any standards or conventions to follow.

## License
Specify the license under which your application is released. This clarifies the permissions and restrictions for users and contributors.

## Acknowledgements
If applicable, acknowledge any external libraries, frameworks, or resources that you have used in the development of your application.

## Contact
Provide contact information for users or contributors to reach out for further assistance or inquiries.
