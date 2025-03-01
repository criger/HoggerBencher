# HoggerBencher

## What is HoggerBencher
HoggerBencher is a C# program, that is able to show you the difference in both CPU and RAM performance when it comes to the following different classes:

* String VS StringBuilder
* Array VS List VS Dictionary

In addition it also has a fun brainteezer test, that can be used to see that something that may seem obvious, not necessarily is obvious..

## How about Java? Can CPU/RAM performance be measured there? 

Short answer: Yes and no
Long answer:
CPU performance is not a problem, but as for RAM performance, it's not possible without a lot of extra hassle. This has to do with the way JVM works.
Extraxting RAM performance data from Java's JVM Garbage Collector is not a straight forward operation,but at least this was far more easier with C#, although with some minor issues. 

## How accurate is the extracted data? 
Very accurate, however... If you see some strange data, try running the test again. 
This happens when NET Garbage Collector suddenly runs in the middle of the benchmark  Even if it is instructed to wait until the test is over. 
This is by design from Microsoft and is nothing we can do anything about. It happens when the system finds out that it needs to free up RAM due to it being needed somewhere else.
