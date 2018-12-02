# Status
This is still pretty alpha, and is only tested with MSSQL. All code is at this point written in the body of the program. This is something I like to do when I am doing some rapid testing. I want to make it work, then make it right. I need this in another project of mine (customization of my i3 desktop). But I also want to share it to everyone else who could need it.

# Description
A small program that allows a person to create a formatted string, with columns that matches a given sql string. The string is then used on all returned rows, and is outputted to the console.

# Example
For now only environment variables is supported. Create a launchSettings.json file and give the following information to the environment section

```
     "environmentVariables": {
        "Database:ConnectionStringTemplate": "Server=server;Initial Catalog=catalog;Persist Security Info=False;User ID={username};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
        "Database:Username": "user",
        "Database:Password": "pass",
        "Database:Sql": "select * from table order by time desc",
        "Input:FormattedInput": "[{Time}] {Identifier} : {Value} {Unit}",
        "Output:Multiline": "false"
      }
```

The above example will connection the database server, and fire the sql, and then, for each row, it will find the columns in the FormattedInput:

1. Time
2. Identifier
3. Value
4. Unit

and map the value for each column to the string, save the output, and in the end write it to the console. The above config will output something on the line of 

```
[02/12/2018 22.04.01] sensor 1 : 21,00000 C
[02/12/2018 22.04.01] sensor 2: 21,00000 C
[02/12/2018 22.04.01] sensor 3 : 22,00000 C
```

Please notice that the multiline feature isn't implemented yet.