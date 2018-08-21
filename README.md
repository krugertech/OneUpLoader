# Screenshot

![alt text](https://raw.githubusercontent.com/krugertech/OneUpLoader/master/UpDemo.png)


# Features

• Multiple simultanous file upload

• Chunked upload (infinite file size)

• Auto resume

• Drag and drop (Chrome and FF)

• Full and partial local file hashes (using Spark Md5)

• IE11+, Firefox and Chrome browser support.


# Get Started

1. Download the vs2015 solution

2. Restore the nuget packages

3. Launch the solution in debug mode

Note: Script debugging while calculating md5 hashes is obviously going to be slow. Fire up Chrome or Firefox to see the real performance.


# How I do integrate this into my project?

There are 2 files you need.

• Copy the FileController.cs

• and Copy the Views/File/Index.cshtml path and file to your solution.
