# Status
This is still pretty alpha, and is only tested with MSSQL. All code is at this point written in the body of the program. This is something I like to do when I am doing some rapid testing. I want to make it work, then make it right. I need this in another project of mine (customization of my i3 desktop). But I also want to share it to everyone else who could need it.

# Description
A small program that allows a person to create a formatted string, with columns that matches a given sql string. The string is then used on all returned rows, and is outputted to the console.

# Example

FOr now only a config file in bin root is supported. Create a config file under a folder named config in the root bin directory and name the config file main.json. The contents of the file should be:

```
{
  "Input": {
    "FormattedInput": "[{MeasurementTime}] {DeviceIdentifier} : {Value} {Unit}"
  },
  "Output": {
    "Multiline": true,
    "Seperator": " "
  },
  "Database": {
    "ConnectionString": "Server=(server);Initial Catalog=(catalog);Persist Security Info=False;User ID=(user);Password=(password);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "Sql": "select * from table order by column desc"
  }
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