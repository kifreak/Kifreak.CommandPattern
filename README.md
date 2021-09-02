## What is Kifreak.CommandPattern
The library helps to implement a Command pattern in your console application

## How it works?

You can check the project Kifreak.CommandPattern.Console to get an example.

Instantiate in your program.cs file the CommandPattrn class.

```c# 
class Program {
        static async Task Main(string[] args)
        {
            await CommandPattern.Execute(args);
        }
    }
```

## Crate your own commands:

One command its one action. For example: 

`myprogram.exe Take photo`

This is a command to take a photo. 

For this action you need to create a class inherits from BaseCommand and override the next methods:

`Task Execute()`: This is your command action (Take a photo).

`bool Validate()`: Validate the params of your command (return a boolean). 
 
`CommandName {get;}`: Your command name. If your command is TAKE you call as: myapp.exe TAKE

`Description {get;}`: Your command description. It used to show the help page.

## Add parameters to your command

If you need to create parameters to your action like:

   `myprogram.exe Take photo --width 1028x640 --black&white`

or
   
  `myprogram.exe Take photo widht1028x640 -b`

Each parameter (Take or photo or --width 1028x640) if a Property in your Command Class

```c#
 public string TakeType {get;set;} //Photo or video
 public string PathToSave {get;set;}
 public string Width {get;set;} //Size of the image or video
 public bool BlackAndWhite {get;set;} //Take the video or photo in black and white.
```

There are attributes to determinate which property belong to which parameter

 `myprogram.exe Take photo C:\myphoto.jpg -w 1028x640 -b`

* myprogram.exe --> Executable
* Take --> CommandName
* Photo --> Main Attribute
* C:\myphoto.jpg --> Sub attribute
* -w 1028x640 --> Short command (long command = 
* --width). Value: 1028x640
* -b --> Short command with no value (long command = --black&white). Value true if it's defined

The library contains 4 different attributes (you can create new just overriding de BaseAttribute class):

MainAttribute --> For the first attribute (in the example: Photo).
```c#
[Main(description: "You can take photo or video")]
public string Photo {get; set;}
```
SubAttribute --> For the second, third, four.... attribute (in the example: the path)
```c#
[Sub(1, description: "Where the photo/video will be save")]
public string PathToSave {get; set;}
```
OptionalAttribute --> An optional attribute (with -- or -).
Accept diferentes parameters. Short and LongCommands and a default value.

```c#
[Optional(showtCommand: "w",longCommand: "width", defaultValue: "1028x640", description: "The size of your image or video")]
public string Width {get; set;}
```
OptionalNoValueAttribute --> An optional attribute withouth value.
```c#
[OptionalNoValue(shortCommand: "b", longCommand: "black&white", description: "Take the photo or video in black and white")]
public bool IsBlackAndWhite {get; set;}
```
